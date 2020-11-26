using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore.Config
{
    internal struct SmsServiceUrls
    {
        /// <summary>
        ///  GoSMS API base url
        /// </summary>
        internal const string BASE_URL = "https://api.gosms.ge/api/";

        /// <summary>
        /// GoSMS API Send message url
        /// </summary>
        internal const string SEND_SMS = "https://api.gosms.ge/api/sendsms";

        /// <summary>
        /// GoSMS API send OTP message url
        /// </summary>
        internal const string SEND_OTP = "https://api.gosms.ge/api/otp/send";

        /// <summary>
        /// GoSMS API check balance url
        /// </summary>
        internal const string CHECK_BALANCE = "https://api.gosms.ge/api/sms-balance";

        /// <summary>
        /// GoSMS API verify OTP url
        /// </summary>
        internal const string VERIFY_OTP = "https://api.gosms.ge/api/otp/verify";

        /// <summary>
        /// GoSMS check message status url
        /// </summary>
        internal const string CHECK_STATUS = "https://api.gosms.ge/api/checksms";

    }
}