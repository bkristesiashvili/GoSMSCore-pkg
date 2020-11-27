using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore
{
    public enum MessageStatus
    {
        /// <summary>
        /// Message successfuly sent
        /// </summary>
        Sent,

        /// <summary>
        /// sent message in progress to delivery
        /// </summary>
        Processing,

        /// <summary>
        /// Message delivered status
        /// </summary>
        Delivered,

        /// <summary>
        /// message send failed
        /// </summary>
        Failed,

        /// <summary>
        /// when nothing response is known
        /// </summary>
        Undefined
    }
}
