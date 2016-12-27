using System;

namespace SparqlCaller.Common
{
    public static class ConversionUtil
    {
        public static DateTime? ConvertToDateTime(string dateString)
        {
            DateTime dateTime;
            return !DateTime.TryParse(dateString, out dateTime)?(DateTime?) null:dateTime;
        }
    }
}
