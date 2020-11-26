using System;

namespace GoSMSCore.Config
{
    public sealed class SmsSettings : ISmsSettings
    {
        #region FILEDS & CONSTRUCTOR

        /// <summary>
        /// Creates SmsSettings object 
        /// </summary>
        /// <param name="configuration">access on application json file</param>
        public SmsSettings(SmsSettingsOption option)
        {
            Sender = option.Sender;
            ApiKey = option.ApiKey;

            BaseUrl = SmsServiceUrls.BASE_URL;
            SendSmsCall = SmsServiceUrls.SEND_SMS;
            CheckStatusCall = SmsServiceUrls.CHECK_STATUS;
            CheckBalanceCall = SmsServiceUrls.CHECK_BALANCE;
            SendOtpCall = SmsServiceUrls.SEND_OTP;
            VerifyOtpCall = SmsServiceUrls.VERIFY_OTP;
        }

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets sms sender name
        /// </summary>
        public string Sender { get; }

        /// <summary>
        /// Gets sms api key string
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Gets Sms service provider url
        /// </summary>
        public string BaseUrl { get; }

        /// <summary>
        /// Send sms api call url
        /// </summary>
        public string SendSmsCall { get; }

        /// <summary>
        /// chec sms status api call url
        /// </summary>
        public string CheckStatusCall { get; }

        /// <summary>
        /// check balance api call url
        /// </summary>
        public string CheckBalanceCall { get; }

        /// <summary>
        /// send OTP sms api call url
        /// </summary>
        public string SendOtpCall { get; }

        /// <summary>
        /// Veryfy OTP api call url
        /// </summary>
        public string VerifyOtpCall { get; }

        #endregion

        #region DIPOSE METHOD

        /// <summary>
        /// Disposes sms settings object
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
