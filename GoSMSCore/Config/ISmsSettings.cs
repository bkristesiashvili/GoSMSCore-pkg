using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore.Config
{
    public interface ISmsSettings : IDisposable
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets sms sender name
        /// 
        /// </summary>
        string Sender { get; }

        /// <summary>
        /// Gets sms api key string
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Gets Sms service provider url
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Send sms api call url
        /// </summary>
        string SendSmsCall { get; }

        /// <summary>
        /// chec sms status api call url
        /// </summary>
        string CheckStatusCall { get; }

        /// <summary>
        /// check balance api call url
        /// </summary>
        string CheckBalanceCall { get; }

        /// <summary>
        /// send OTP sms api call url
        /// </summary>
        string SendOtpCall { get; }

        /// <summary>
        /// Veryfy OTP api call url
        /// </summary>
        string VerifyOtpCall { get; }

        #endregion
    }
}
