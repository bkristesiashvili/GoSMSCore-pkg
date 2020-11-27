using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore.Responses
{
    #region BASE RESPONSES FOR SEND & DELIVERY

    public interface IErrorResponse
    {
        /// <summary>
        /// Get error code if sms not sent
        /// </summary>
        int ErrorCode { get; }

        /// <summary>
        /// Get error message
        /// </summary>
        string Message { get; }
    }

    public interface IResponse : IErrorResponse
    {
        /// <summary>
        /// Balance check success
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Sender
        /// </summary>
        string From { get; }

        /// <summary>
        /// Recipient
        /// </summary>
        string To { get; }

        /// <summary>
        /// Sent Message Text
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Sent Date Time
        /// </summary>
        DateTime SendAt { get; }

        /// <summary>
        /// Message Encoding
        /// </summary>
        string Encode { get; }

        /// <summary>
        /// Segment
        /// </summary>
        int Segment { get; }

        /// <summary>
        /// message characters count
        /// </summary>
        int SmsCharacters { get; }
    }

    public abstract class BaseResponse : IResponse
    {
        /// <summary>
        /// Balance check success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Get error code if sms not sent
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Get error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Recipient
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Sent Message Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Sent Date Time
        /// </summary>
        public DateTime SendAt { get; set; }

        /// <summary>
        /// Message Encoding
        /// </summary>
        public string Encode { get; set; }

        /// <summary>
        /// Segment
        /// </summary>
        public int Segment { get; set; }

        /// <summary>
        /// message characters count
        /// </summary>
        public int SmsCharacters { get; set; }
    }

    #endregion

    #region SMS SEND RESPONSE

    public interface ISmsSendResponse
    {
        /// <summary>
        /// Get sent message id
        /// </summary>
        int Message_Id { get; }

        /// <summary>
        /// Current Balance
        /// </summary>
        int Balance { get;}
    }

    public sealed class SmsSendResponse : BaseResponse, ISmsSendResponse
    {
        /// <summary>
        /// Get sent message id
        /// </summary>
        public int Message_Id { get; set; }

        /// <summary>
        /// Current Balance
        /// </summary>
        public int Balance { get; set; }
    }

    #endregion

    #region BALANCE CHECK RESPONSE

    internal sealed class CheckBalanceResponse 
    {
        /// <summary>
        /// expected current Balance
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Balance check success
        /// </summary>
        public bool Success { get; set; }
    }

    #endregion

    #region DELIVERY RESPONSE

    public interface IDeliveryResponse
    {
        /// <summary>
        /// Get sent message id
        /// </summary>
        int MessageId { get; }

        /// <summary>
        /// Get message status
        /// </summary>
        string Status { get; }
    }

    public sealed class DeliveryResponse: BaseResponse, IDeliveryResponse
    {
        /// <summary>
        /// Get sent message id
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Get message status
        /// </summary>
        public string Status { get; set; }
    }

    #endregion

    #region OPT RESPONSE

    public interface IOtpResponse : IErrorResponse
    {
        /// <summary>
        /// Balance check success
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Get Hash key
        /// </summary>
        string Hash { get; }

        /// <summary>
        /// Sender
        /// </summary>
        string From { get; }

        /// <summary>
        /// Sent Date Time
        /// </summary>
        DateTime SendAt { get; }

        /// <summary>
        /// Message Encoding
        /// </summary>
        string Encode { get; }

        /// <summary>
        /// Segment
        /// </summary>
        int Segment { get; }

        /// <summary>
        /// message characters count
        /// </summary>
        int SmsCharacters { get; }
    }

    public sealed class OtpResponse : IOtpResponse
    {
        /// <summary>
        /// Get error code if sms not sent
        /// </summary>
       public  int ErrorCode { get; set; }

        /// <summary>
        /// Get error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Balance check success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Get Hash key
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Sent Date Time
        /// </summary>
        public DateTime SendAt { get; set; }

        /// <summary>
        /// Message Encoding
        /// </summary>
        public string Encode { get; set; }

        /// <summary>
        /// Segment
        /// </summary>
        public int Segment { get; set; }

        /// <summary>
        /// message characters count
        /// </summary>
        public int SmsCharacters { get; set; }
    }

    #endregion
}