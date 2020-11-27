using GoSMSCore.Responses;

using System;

namespace GoSMSCore
{
    public sealed class SmsDeliveryEventArgs : EventArgs
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Creates sms delivery event args object
        /// </summary>
        /// <param name="_response">delivery response object</param>
        public SmsDeliveryEventArgs(IDeliveryResponse _response) 
        {
            Responses = _response as DeliveryResponse;

            if (_response != null)
                if (((DeliveryResponse)_response).Success) Status = _response.Status.Equals("DELIVERED") ? 
                        MessageStatus.Delivered : _response.Status.Equals("IN_PROGRESS") ? MessageStatus.Processing : MessageStatus.Sent;
                else Status = MessageStatus.Failed;
            else Status = MessageStatus.Undefined;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets delivery response 
        /// </summary>
        public DeliveryResponse Responses { get; }

        /// <summary>
        /// Gets message status
        /// </summary>
        public MessageStatus Status { get; }

        #endregion
    }
}
