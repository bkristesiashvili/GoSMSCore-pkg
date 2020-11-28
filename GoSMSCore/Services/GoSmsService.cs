using GoSMSCore.Config;
using GoSMSCore.Globals;
using GoSMSCore.Helper;
using GoSMSCore.Responses;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoSMSCore
{
    public sealed class GoSmsService : ISmsService
    {
        #region FILEDS & CONSTRUCTOR

        /// <summary>
        /// Creates GoSMS Service object
        /// </summary>
        /// <param name="settings">service configurations access</param>
        public GoSmsService(ISmsSettings settings) => Settings = settings;
        
        #endregion

        #region EVENTS

        /// <summary>
        /// Sent event occured when call send sms method 
        /// </summary>
        public event EventHandler<SmsSendEventArgs> Sent;

        /// <summary>
        /// delivered event occured when call the check message status method
        /// </summary>
        public  event EventHandler<SmsDeliveryEventArgs> Delivered;

        /// <summary>
        /// OTP message sent event
        /// </summary>
        public event EventHandler<OtpEventArgs> SentOTP;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Get Sms configurations settings
        /// </summary>
        public ISmsSettings Settings { get; }

        #endregion

        #region PUBLIC METHODS OF THE INTERFACE

        #region SEND TO ONE SYNC & ASYNC

        /// <summary>
        /// Sends sms to the one number only
        /// </summary>
        /// <param name="number">phone number</param>
        public ISmsSendResponse SendToOne(string number, string message)
        {
            try
            {
                return SendToOneAsync(number, message).Result;
            }
            catch { throw; }
        }

        /// <summary>
        /// Send sms to one number async method
        /// </summary>
        /// <param name="number">recipient phone number</param>
        /// <param name="message">message text</param>
        /// <param name="token">cancellation token struct</param>
        /// <returns></returns>
        public async Task<ISmsSendResponse> SendToOneAsync(string number, string message, CancellationToken token = default)
        {
            try
            {
                return await Task.FromResult(SendMessageToNumber(number, message, token));
            }
            catch { throw; }
        }

        #endregion

        #region SEND TO MANY SYNC & ASYNC

        /// <summary>
        /// Sends sms to many phone numbers synchronously
        /// </summary>
        /// <param name="numbers">phone numbers array</param>
        /// <param name="message">message tex</param>
        /// <param name="millisecond">delay in milliseconds between numbers</param>
        public void SendToMany(IEnumerable<string> numbers, string message, int millisecond = 500)
        {
            try
            {
                SendToManyAsync(numbers, message, millisecond)
                    .Wait();
            }
            catch { throw; }
        }

        /// <summary>
        /// send sms to many recipient async method
        /// </summary>
        /// <param name="numbers">recipient numbers array</param>
        /// <param name="message">message text</param>
        /// <param name="millisecond">delay in milliseconds between numbers</param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public async Task SendToManyAsync(IEnumerable<string> numbers,
            string message, int millisecond = 500, CancellationToken token = default)
        {
            try
            {
                millisecond = millisecond < 500 || millisecond > 10000 ? 500 : millisecond;

                foreach (var number in numbers)
                {
                    await Task.Delay(millisecond);
                    await SendToOneAsync(number, message, token);
                }
            }
            catch { throw; }
        }

        #endregion

        #region OTP METHODS

        /// <summary>
        /// Send OTP message to the number
        /// </summary>
        /// <param name="number">recipient number</param>
        /// <returns></returns>
        public async Task<string> SendOTP(string number)
        {
            try
            {
                var data = new
                {
                    api_key = Settings.ApiKey,
                    phone = number
                };

                HttpResponseMessage response = default;

                using (var httpClient = new HttpClient())
                    response = await httpClient.PostAsync(Settings.SendOtpCall,
                        new StringContent(data.AsJson(), Encoding.UTF8, "Application/Json"));

                var result = JsonConvert.DeserializeObject<OtpResponse>(await response.Content.ReadAsStringAsync());

                OnSentOTP(new OtpEventArgs(result));

                return result.Hash;
            }
            catch { throw; }
        }

        #endregion

        #region CHECK BALANCE & CHECK MESSAGE STATUS METHODS

        /// <summary>
        /// Checks sms balance to the sms service provider
        /// </summary>
        /// <returns></returns>
        public int CheckBalance()
        {
            var data = new
            {
                api_key = Settings.ApiKey
            };

            HttpResponseMessage response = default;

            using (var httpClient = new HttpClient())
                response = httpClient.PostAsync(Settings.CheckBalanceCall,
                    new StringContent(data.AsJson(), Encoding.UTF8, "Application/Json")).Result;

            var result = JsonConvert.DeserializeObject<CheckBalanceResponse>(response.Content.ReadAsStringAsync().Result);

            return result.Success ? result.Balance : 0;
        }

        /// <summary>
        /// Checks sms status by message id
        /// </summary>
        /// <param name="messageId">sent sms id</param>
        /// <returns></returns>
        public string CheckMessageStatus(int messageId)
        {
            try
            {
                var result = GetMessageStatus(messageId);

                return result.Status;
            }
            catch { throw; }
        }

        #endregion

        #region DISPOSE METHOD

        /// <summary>
        /// Dispose GoSMS Service object
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

        #region PRIVATE EVENT HANDLER METHODS

        private IDeliveryResponse GetMessageStatus(int messageId)
        {
            try
            {
                var data = new
                {
                    api_key = Settings.ApiKey,
                    messageId = messageId
                };

                HttpResponseMessage response = default;

                using (var httpClient = new HttpClient())
                    response = httpClient.PostAsync(Settings.CheckStatusCall,
                        new StringContent(data.AsJson(), Encoding.UTF8, "Application/Json")).Result;

                return JsonConvert.DeserializeObject<DeliveryResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch { throw; }
        }

        /// <summary>
        /// Send message async method 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private ISmsSendResponse SendMessageToNumber(string number, string message, CancellationToken token = default)
        {
            try
            {
                if (!number.IsMatch(PackageGlobals.NUMBERS_ONLY))
                    throw new ArgumentException($"{ nameof(number) } Invalid input!");

                if (token != null && token.IsCancellationRequested)
                    return new SmsSendResponse
                    {
                        ErrorCode = 100,
                        Message = "message send canceled!"
                    };

                var data = new
                {
                    api_key = Settings.ApiKey,
                    from = Settings.Sender,
                    to = number,
                    text = message
                };

                HttpResponseMessage response = default;

                StringContent content = new StringContent(data.AsJson(), Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                    response = httpClient.PostAsync(Settings.SendSmsCall, content).Result;

                var responseData = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<SmsSendResponse>(responseData);

                OnSent(new SmsSendEventArgs(result));

                return result;
            }
            catch { throw; }
        }

        /// <summary>
        /// Onsent event handling and invoking
        /// </summary>
        /// <param name="args"></param>
        private void OnSent(SmsSendEventArgs args)
        {
            try
            {
                Sent += (sender, e) =>
                 {
                     if (e.Status == MessageStatus.Sent)
                     {
                         var result = GetMessageStatus(e.Response.Message_Id);

                         OnDelivered(new SmsDeliveryEventArgs(result));
                     }
                 };

                Sent?.Invoke(this, args);
            }
            catch { throw; }
        }

        /// <summary>
        /// on delivered event handling and invoking
        /// </summary>
        /// <param name="args"></param>
        private void OnDelivered(SmsDeliveryEventArgs args)
        {
            try
            {
                if (args.Responses.MessageId > 0)
                    Delivered?.Invoke(this, args);
            }
            catch { throw; }
        }

        /// <summary>
        /// Invokes sent otp event
        /// </summary>
        /// <param name="args"></param>
        private void OnSentOTP(OtpEventArgs args)
        {
            try
            {
                SentOTP?.Invoke(this, args);
            }
            catch { throw; }
        }

        #endregion
    }
}
