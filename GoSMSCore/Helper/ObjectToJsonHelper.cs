using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace GoSMSCore.Helper
{
    internal static class ObjectToJsonHelper
    {
        /// <summary>
        /// Convert objev to Json format string
        /// </summary>
        /// <param name="_object">json formatable object</param>
        /// <returns></returns>
        internal static string AsJson(this object _object)
        {
            return _object == null ? string.Empty : JsonConvert.SerializeObject(_object);
        }
    }
}
