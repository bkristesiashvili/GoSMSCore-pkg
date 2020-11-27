using GoSMSCore.Responses;

using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore
{
    public sealed class OtpEventArgs : EventArgs
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Creates OtpEventArgs object
        /// </summary>
        /// <param name="_response">otp response message parameter</param>
        public OtpEventArgs(IOtpResponse _response)
        {
            Response = _response as OtpResponse;

            Status = _response == null ? MessageStatus.Undefined : _response.Success ? MessageStatus.Sent : MessageStatus.Failed;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Otp response message 
        /// </summary>
        public OtpResponse Response { get; }

        /// <summary>
        /// Send OTP message status
        /// </summary>
        public MessageStatus Status { get; }

        #endregion
    }
}
