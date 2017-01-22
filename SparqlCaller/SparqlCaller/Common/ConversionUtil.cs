using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SparqlCaller.Common
{
    public static class ConversionUtil
    {
        private static Regex regex = new Regex(@"\@[a-zA-Z]{2}$");
        private static Regex regexLanguage = new Regex(@"\@en$");
        private static Regex regexDigit = new Regex(@"(^[0-9,\.]+)\^\^");
        private static Regex regexYear = new Regex(@"\((\d{4})");

        public static DateTime? ConvertToDateTime(string dateString)
        {
            DateTime dateTime;
            if (dateString == null)
                return null;
            var match = regexYear.Match(dateString);
            if (!match.Success)
                return null;
            var yearToParse = match.Groups[1].ToString();
            return DateTime.ParseExact(yearToParse, "yyyy", CultureInfo.InvariantCulture);
        }

        public static double ConvertToDouble(string numString)
        {
            double number;
            if (numString == null)
                return double.NaN;
            var match = regexDigit.Match(numString);
            if (!match.Success)
                return double.NaN;
            var numberToParse = match.Groups[1].ToString();
            return !double.TryParse(numberToParse, NumberStyles.Number, CultureInfo.InvariantCulture, out number) ? double.NaN : number;
        }

        public static string RemoveLanguageLabel(this string textToProcess)
        {
            if (string.IsNullOrEmpty(textToProcess))
                return string.Empty;
            return regex.Replace(textToProcess,"");
        }

        public static bool IsEnglish(this string textToProcess)
        {
            if (string.IsNullOrEmpty(textToProcess))
                return true;
            var match = regexLanguage.Match(textToProcess);
            return match.Success;
        }
    }
}
