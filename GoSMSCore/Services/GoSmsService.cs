using GoSMSCore.Config;
using GoSMSCore.Globals;
using GoSMSCore.Helper;
using GoSMSCore.Responses;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GoSMSCore.Services
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
        public IGoSmsResponse SendTo(string number, string message)
        {
            try
            {
                if (!number.IsMatch(PackageGlobals.NUMBERS_ONLY))
                    throw new ArgumentException($"{ nameof(number) } Invalid input!");

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

                return JsonConvert.DeserializeObject<GoSmsResponse>(responseData);
            }
            catch{ throw; }
        }

        /// <summary>
        /// Sends sms to many phone numbers
        /// </summary>
        /// <param name="numbers">phone numbers</param>
        public IEnumerable<IGoSmsResponse> SendTo(IEnumerable<string> numbers, string message)
        {
            try
            {
                var responses = new List<IGoSmsResponse>();

                foreach (var number in numbers)
                    responses.Add(SendTo(number, message));

                return responses;
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
        public IDeliveryResponse CheckMessageStatus(int messageId)
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
    }
}
