using GoSMSCore.Responses;

using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore.Services
{
    public interface ISmsService : IDisposable
    {
        #region PUBLIC METHODS OF THE INTERFACE

        /// <summary>
        /// Sends sms to the one number only
        /// </summary>
        /// <param name="number">phone number</param>
        IGoSmsResponse SendTo(string number, string message);

        /// <summary>
        /// Sends sms to many phone numbers
        /// </summary>
        /// <param name="numbers">phone numbers</param>
        IEnumerable<IGoSmsResponse> SendTo(IEnumerable<string> numbers, string mesage);

        /// <summary>
        /// Checks sms balance to the sms service provider
        /// </summary>
        /// <returns></returns>
        ICheckBalanceResponse CheckBalance();

        /// <summary>
        /// Checks sms status by message id
        /// </summary>
        /// <param name="messageId">sent sms id</param>
        /// <returns></returns>
        IDeliveryResponse CheckMessageStatus(int messageId);

        #endregion
    }
}
