using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RCBLibrary
{
    public static class SanitizerExtension
    {
        public static string SanitizeId(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            // 1. Lowercase and Trim leading/trailing whitespace
            string result = input.Trim().ToLower();

            // 2. Replace internal whitespaces (one or more) with a single underscore
            result = Regex.Replace(result, @"\s+", "_");

            // 3. Remove anything that isn't a lowercase letter or an underscore
            result = Regex.Replace(result, @"[^a-z_]", "");

            return result;
        }

    }
}
