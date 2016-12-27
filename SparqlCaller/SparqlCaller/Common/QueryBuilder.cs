using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparqlCaller.Common
{
    public static class QueryBuilder
    {
        private static Dictionary<FilterType, string> FilterDictionary = new Dictionary<FilterType, string>();

        static QueryBuilder()
        {
            InitializeDictionary();
        }

        public static void CreateQuery(FilterType filterType, params string[] queryParameters)
        {
            if(queryParameters == null || queryParameters.Length == 0)
                return;

            
        }

        private static void InitializeDictionary()
        {
            FilterDictionary.Add(FilterType.Country, Queries.FilterSnippets.CountryFilter);
            FilterDictionary.Add(FilterType.Director, Queries.FilterSnippets.DirectorFilter);
            FilterDictionary.Add(FilterType.Date, Queries.FilterSnippets.DateFilter);
            FilterDictionary.Add(FilterType.Writer, Queries.FilterSnippets.WriterFilter);
            FilterDictionary.Add(FilterType.Genre, Queries.FilterSnippets.GenreFilter);
        }
    }
}
