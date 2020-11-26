using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore.Config
{
    public sealed class SmsSettingsOption
    {
        /// <summary>
        /// Get or Set Sms Service api key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Get or Set sender name
        /// </summary>
        public string Sender { get; set; }
    }
}
