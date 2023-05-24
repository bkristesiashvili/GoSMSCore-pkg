using GoSMSCore.Responses;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoSMSCore
{
    public interface ISmsService : IDisposable
    {
        #region PUBLIC METHODS OF THE INTERFACE

        #region SEND TO ONE SYNC & ASYNC

        /// <summary>
        /// Sends sms to the one number only
        /// </summary>
        /// <param name="number">phone number</param>
        ISmsSendResponse SendToOne(string number, string message, bool urgent = false);

        /// <summary>
        /// Send sms to one number async method
        /// </summary>
        /// <param name="number">recipient phone number</param>
        /// <param name="message">message text</param>
        /// <param name="token">cancellation token struct</param>
        /// <returns></returns>
        Task<ISmsSendResponse> SendToOneAsync(string number, string message, CancellationToken token = default, bool urgent = false);

        #endregion

        #region SEND TO MANY SYNC & ASYNC

        /// <summary>
        /// Sends sms to many phone numbers
        /// </summary>
        /// <param name="numbers">phone numbers</param>
        void SendToMany(IEnumerable<string> numbers, string mesage, int millisecond = 500, bool urgent = false);

        /// <summary>
        /// send sms to many recipient async method
        /// </summary>
        /// <param name="numbers">recipients numbers</param>
        /// <param name="message">message text</param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        Task SendToManyAsync(IEnumerable<string> numbers,
            string message, int millisecond = 500, CancellationToken token = default, bool urgent = false);

        #endregion

        #region OTP METHODS

        /// <summary>
        /// Send OTP message to the number
        /// </summary>
        /// <param name="number">recipient number</param>
        /// <returns></returns>
        Task<string> SendOTP(string number);

        #endregion

        /// <summary>
        /// Checks sms balance to the sms service provider
        /// </summary>
        /// <returns></returns>
        int CheckBalance();

        /// <summary>
        /// Checks sms status by message id
        /// </summary>
        /// <param name="messageId">sent sms id</param>
        /// <returns></returns>
        string CheckMessageStatus(int messageId);

        #endregion

        #region EVENTS

        /// <summary>
        /// Sent event occured when call send sms method 
        /// </summary>
        event EventHandler<SmsSendEventArgs> Sent;

        /// <summary>
        /// delivered event occured when call the check message status method
        /// </summary>
        event EventHandler<SmsDeliveryEventArgs> Delivered;

        /// <summary>
        /// OTP message sent event
        /// </summary>
        event EventHandler<OtpEventArgs> SentOTP;
 
        #endregion
    }
}
