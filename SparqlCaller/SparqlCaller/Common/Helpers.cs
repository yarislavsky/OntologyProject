using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace SparqlCaller.Common
{
    public static class Helpers
    {
        public static string GetValue(this SparqlResult sResult, string property)
        {
            return sResult.Any(res => res.Key == property)
                ? sResult.FirstOrDefault(res => res.Key == property).Value?.ToString()
                : string.Empty;
        }
    }
}
