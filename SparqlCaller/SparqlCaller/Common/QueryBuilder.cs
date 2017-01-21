using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparqlCaller.Common
{
    public static class QueryBuilder
    {
        private static Dictionary<FilterType, string> FilterMdbDictionary = new Dictionary<FilterType, string>();
        private static Dictionary<FilterType, string> FilterDbpDictionary = new Dictionary<FilterType, string>();


        static QueryBuilder()
        {
            InitializeMdbDictionary();
            InitializeDbpDictionary();
        }

        public static string CreateQuery(string sourceType, FilterType filterType,
            int? rowLimit,
            string countryFilter = "",
            string directorFilter = "",
            string dateFilter = "",
            string writerFilter = "",
            string genreFilter = "")
        {
            string filterString;
            string selectionBase;

            switch (sourceType)
            {
                case Consts.URL.DbPedia:
                    filterString = CreateFilterQuery(FilterDbpDictionary, filterType, countryFilter, directorFilter,
                        dateFilter, writerFilter, genreFilter);
                    selectionBase = Queries.Templates.SelectBaseDbPedia;
                    break;
                case Consts.URL.LinkedMdb:
                    filterString = CreateFilterQuery(FilterMdbDictionary, filterType, countryFilter, directorFilter,
                        dateFilter, writerFilter, genreFilter);
                    selectionBase = Queries.Templates.SelectBase;
                    break;
                default:
                    throw new NotImplementedException();
            }

            var mainQuery = Queries.Templates.AllPrefixes + selectionBase + filterString + "} } "
                            + (rowLimit.HasValue ? $"LIMIT {rowLimit.Value}" : "");

            return mainQuery;
        }

        private static string CreateFilterQuery(Dictionary<FilterType, string> dictionary, FilterType filterType, string countryFilter, string directorFilter, string dateFilter, string writerFilter, string genreFilter)
        {
            string filterString = string.Empty;
            if ((filterType & FilterType.Country) != 0)
                filterString += GetFormatedSnipped(dictionary[FilterType.Country], countryFilter);
            else
                filterString += GetFormatedSnipped(dictionary[FilterType.Country], Consts.Optional);
            if ((filterType & FilterType.Director) != 0)
                filterString += GetFormatedSnipped(dictionary[FilterType.Director], directorFilter);
            else
                filterString += GetFormatedSnipped(dictionary[FilterType.Director], Consts.Optional);
            if ((filterType & FilterType.Date) != 0)
                filterString += GetFormatedSnipped(dictionary[FilterType.Date], dateFilter);
            else
                filterString += GetFormatedSnipped(dictionary[FilterType.Date], Consts.Optional);
            if ((filterType & FilterType.Writer) != 0)
                filterString += GetFormatedSnipped(dictionary[FilterType.Writer], writerFilter);
            else
                filterString += GetFormatedSnipped(dictionary[FilterType.Writer], Consts.Optional);
            if ((filterType & FilterType.Genre) != 0)
                filterString += GetFormatedSnipped(dictionary[FilterType.Genre], genreFilter);
            else
                filterString += GetFormatedSnipped(dictionary[FilterType.Genre], Consts.Optional);
            return filterString;
        }

        private static string GetFormatedSnipped(string stringToFormat, string filterValue)
        {
            string snipped;
            if (filterValue == Consts.Optional)
            {
                snipped = string.Format(stringToFormat, " ");
                snipped = "OPTIONAL {" + snipped + "}";
            }
            else
            {
                snipped = string.Format(stringToFormat, filterValue);
            }
            return snipped;
        }

        private static void InitializeMdbDictionary()
        {
            FilterMdbDictionary.Add(FilterType.Country, Queries.FilterSnippets.CountryFilter);
            FilterMdbDictionary.Add(FilterType.Director, Queries.FilterSnippets.DirectorFilter);
            FilterMdbDictionary.Add(FilterType.Date, Queries.FilterSnippets.DateFilter);
            FilterMdbDictionary.Add(FilterType.Writer, Queries.FilterSnippets.WriterFilter);
            FilterMdbDictionary.Add(FilterType.Genre, Queries.FilterSnippets.GenreFilter);
        }

        private static void InitializeDbpDictionary()
        {
            FilterDbpDictionary.Add(FilterType.Country, Queries.FilterSnippets.CountryFilterDbPedia);
            FilterDbpDictionary.Add(FilterType.Director, Queries.FilterSnippets.DirectorFilterDbPedia);
            FilterDbpDictionary.Add(FilterType.Date, Queries.FilterSnippets.DateFilterDbPedia);
            FilterDbpDictionary.Add(FilterType.Writer, Queries.FilterSnippets.WriterFilterDbPedia);
            FilterDbpDictionary.Add(FilterType.Genre, Queries.FilterSnippets.GenreFilterDbPedia);

        }
    }
}
