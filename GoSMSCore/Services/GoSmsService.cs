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
        public GoSmsService(ISmsSettings settings)
        {
            Settings = settings;
        }

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

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Get Sms configurations settings
        /// </summary>
        public ISmsSettings Settings { get; }

        #endregion

        #region PUBLIC METHODS OF THE INTERFACE

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

        /// <summary>
        /// Sends sms to many phone numbers
        /// </summary>
        /// <param name="numbers">phone numbers</param>
        public void SendToMany(IEnumerable<string> numbers, string message)
        {
            try
            {
                SendToManyAsync(numbers, message)
                    .Wait();
            }
            catch { throw; }
        }

        /// <summary>
        /// send sms to many recipient async method
        /// </summary>
        /// <param name="numbers">recipients numbers</param>
        /// <param name="message">message text</param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public async Task SendToManyAsync(IEnumerable<string> numbers,
            string message, CancellationToken token = default)
        {
            try
            {
                foreach (var number in numbers)
                    await SendToOneAsync(number, message, token);
            }
            catch { throw; }
        }

        /// <summary>
        /// Checks sms balance to the sms service provider
        /// </summary>
        /// <returns></returns>
        public ICheckBalanceResponse CheckBalance()
        {
            var data = new
            {
                api_key = Settings.ApiKey
            };

            HttpResponseMessage response = default;

            using (var httpClient = new HttpClient())
                response = httpClient.PostAsync(Settings.CheckBalanceCall,
                    new StringContent(data.AsJson(), Encoding.UTF8, "Application/Json")).Result;

            return JsonConvert.DeserializeObject<CheckBalanceResponse>(response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Checks sms status by message id
        /// </summary>
        /// <param name="messageId">sent sms id</param>
        /// <returns></returns>
        public async Task<IDeliveryResponse> CheckMessageStatus(int messageId)
        {
            var data = new
            {
                api_key = Settings.ApiKey,
                messageId = messageId
            };

            HttpResponseMessage response = default;

            using (var httpClient = new HttpClient())
                response = await httpClient.PostAsync(Settings.CheckStatusCall,
                    new StringContent(data.AsJson(), Encoding.UTF8, "Application/Json"));

            var result = JsonConvert.DeserializeObject<DeliveryResponse>(await response.Content.ReadAsStringAsync());

            return result;
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

        #region PRIVATE EVENT HANDLER METHODS

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
                         var result = CheckMessageStatus(e.Response.Message_Id).Result;

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
                if (args.Responses?.MessageId > 0l)
                    Delivered?.Invoke(this, args);
            }
            catch { throw; }
        }

        #endregion
    }
}
