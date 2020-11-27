using GoSMSCore.Responses;

using System;

namespace GoSMSCore
{
    public sealed class SmsSendEventArgs : EventArgs
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Creates sms send event args object
        /// </summary>
        /// <param name="_response"></param>
        internal SmsSendEventArgs(ISmsSendResponse _response)
        {
            Response = _response as SmsSendResponse;

            if (_response != null) Status =((SmsSendResponse) _response).Success ? MessageStatus.Sent : MessageStatus.Failed;
            else Status = MessageStatus.Undefined;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// request send sms response
        /// </summary>
        public SmsSendResponse Response { get; }

        /// <summary>
        /// Gets message status
        /// </summary>
        public MessageStatus Status { get; }

        #endregion
    }
}
