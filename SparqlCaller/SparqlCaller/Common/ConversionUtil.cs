using System;
using System.Text.RegularExpressions;

namespace SparqlCaller.Common
{
    public static class ConversionUtil
    {
        private static Regex regex = new Regex(@"\@[a-zA-Z]{2}$");

        public static DateTime? ConvertToDateTime(string dateString)
        {
            DateTime dateTime;
            return !DateTime.TryParse(dateString, out dateTime)?(DateTime?) null:dateTime;
        }

        public static string RemoveLanguageLabel(this string textToProcess)
        {
            if (string.IsNullOrEmpty(textToProcess))
                return string.Empty;
            return regex.Replace(textToProcess,"");
        }
    }
}
