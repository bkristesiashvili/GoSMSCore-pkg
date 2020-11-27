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
        /// message send failed
        /// </summary>
        Failed,

        /// <summary>
        /// Message delivered status
        /// </summary>
        Delivered,

        /// <summary>
        /// when nothing response is known
        /// </summary>
        Undefined
    }
}
