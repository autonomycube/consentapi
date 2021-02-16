using System;
using System.Collections.Generic;
using System.Text;

namespace Consent.Api.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string dateString)
        {
            DateTime resultDate;
            if (DateTime.TryParse(dateString, out resultDate))
                return resultDate;

            return default;
        }

        public static DateTime? ToNullableDateTime(this string dateString)
        {
            if (string.IsNullOrEmpty((dateString ?? "").Trim()))
                return null;

            DateTime resultDate;
            if (DateTime.TryParse(dateString, out resultDate))
                return resultDate;

            return null;
        }

        public static int ToInt32(this string value, int defaultIntValue = 0)
        {
            int parsedInt;
            if (int.TryParse(value, out parsedInt))
            {
                return parsedInt;
            }

            return defaultIntValue;
        }

        public static int? ToNullableInt32(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToInt32();
        }

        public static long ToInt64(this string value, long defaultInt64Value = 0)
        {
            long parsedInt64;
            if (Int64.TryParse(value, out parsedInt64))
            {
                return parsedInt64;
            }

            return defaultInt64Value;
        }

        public static long? ToNullableInt64(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.ToInt64();
        }

        /// <summary>
        /// Custom for sms template
        /// </summary>
        /// <param name="str">String with placeholders</param>
        /// <param name="args">key-value of Placeholder</param>
        /// <returns></returns>
        public static string Format(this string str, Dictionary<string, string> parameters)
        {            
            var sb = new StringBuilder(str);
            foreach (var kv in parameters)
            {
                sb.Replace("{{" + kv.Key + "}}", kv.Value != null ? kv.Value.ToString() : "");
            }

            return sb.ToString();
        }
    }
}
