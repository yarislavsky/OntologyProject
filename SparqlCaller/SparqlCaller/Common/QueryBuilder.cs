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

        public static string CreateQuery(FilterType filterType, string countryFilter = "",
            string directorFilter = "", string dateFilter = "", string writerFilter = "", string genreFilter = "")
        {
            
            string filterString = string.Empty;

            if ((filterType & FilterType.Country) != 0)
                filterString +=(string.Format(FilterDictionary[FilterType.Country], countryFilter));
            if ((filterType & FilterType.Director) != 0)
                filterString += (string.Format(FilterDictionary[FilterType.Director], directorFilter));
            if ((filterType & FilterType.Date) != 0)
                filterString += (string.Format(FilterDictionary[FilterType.Date], dateFilter));
            if ((filterType & FilterType.Writer) != 0)
                filterString += (string.Format(FilterDictionary[FilterType.Writer], writerFilter));
            if ((filterType & FilterType.Genre) != 0)
                filterString += (string.Format(FilterDictionary[FilterType.Genre], genreFilter));

            var mainQuery = Queries.Templates.AllPrefixes + Queries.Templates.SelectBase + filterString + "} }";

            return mainQuery;
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
