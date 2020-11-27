using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore.Responses
{

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

     public interface ISmsSendResponse : IErrorResponse
    {
        /// <summary>
        /// get true if message sent successfuly
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Get sent message id
        /// </summary>
        int Message_Id { get; }

        /// <summary>
        /// Sender
        /// </summary>
         string From { get;  }

        /// <summary>
        /// Recipient
        /// </summary>
         string To { get;  }

        /// <summary>
        /// Sent Message Text
        /// </summary>
         string Text { get;  }

        /// <summary>
        /// Sent Date Time
        /// </summary>
         DateTime SendAt { get;  }

        /// <summary>
        /// Current Balance
        /// </summary>
         int Balance { get;  }

        /// <summary>
        /// Message Encoding
        /// </summary>
         string Encode { get;  }

        /// <summary>
        /// Segment
        /// </summary>
         int Segment { get;  }

        /// <summary>
        /// message characters count
        /// </summary>
         int SmsCharacters { get;  }
    }

    public sealed class SmsSendResponse : ISmsSendResponse
    {
        internal SmsSendResponse() { }

        /// <summary>
        /// Get error code if sms not sent
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Get error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// get true if message sent successfuly
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Get sent message id
        /// </summary>
        public int Message_Id { get; set; }

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
        /// Current Balance
        /// </summary>
        public int Balance { get; set; }

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

    public interface ICheckBalanceResponse : IErrorResponse
    {
        /// <summary>
        /// Balance check success
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// expected current Balance
        /// </summary>
        int Balance { get; }
    }

    public sealed class CheckBalanceResponse : ICheckBalanceResponse
    {
        internal CheckBalanceResponse() { }

        /// <summary>
        /// Get error code if sms not sent
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Get error message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Balance check success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// expected current Balance
        /// </summary>
        public int Balance { get; set; }
    }

    public interface IDeliveryResponse : IErrorResponse
    {
        /// <summary>
        /// get true if message sent successfuly
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Get sent message id
        /// </summary>
        int MessageId { get; }

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

        /// <summary>
        /// Get message status
        /// </summary>
        string Status { get; }
    }

    public sealed class DeliveryResponse: IDeliveryResponse
    {
        internal DeliveryResponse() { }

        /// <summary>
        /// Get error code if sms not sent
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Get error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// get true if message sent successfuly
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Get sent message id
        /// </summary>
        public int MessageId { get; set; }

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

        /// <summary>
        /// Get message status
        /// </summary>
        public string Status { get; set; }
    }
}