using MainModule.Models;
using SparqlCaller;
using SparqlCaller.Common;
using SparqlCaller.Entities;

namespace MainModule.Managers
{
    public class QueryImplemantationManager
    {
        public void ImplementQuery(QueryParameters queryParameters)
        {
            FilterType filterType = 0;
            if (queryParameters.IsCountryChecked)
                filterType |= FilterType.Country;
            if (queryParameters.IsDateChecked)
                filterType |= FilterType.Date;
            if (queryParameters.IsDirectorChecked)
                filterType |= FilterType.Director;
            if (queryParameters.IsGenreChecked)
                filterType |= FilterType.Genre;
            if (queryParameters.IsWriterChecked)
                filterType |= FilterType.Writer;

            var allMovies = SparqlQueryBase.GetEntities<Movie>(Consts.URL.DbPedia, QueryBuilder.CreateQuery(filterType));
        }
    }
}