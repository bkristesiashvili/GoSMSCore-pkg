using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GoSMSCore.Helper
{
    internal static class StringExtensionMethodHelper
    {
        /// <summary>
        /// Regex pattern check on the string
        /// </summary>
        /// <param name="text">input string</param>
        /// <param name="regexPattern">regex pattern check</param>
        /// <returns></returns>
        public static bool IsMatch(this string text, string regexPattern)
        {
            return Regex.IsMatch(text, regexPattern);
        }
    }
}
