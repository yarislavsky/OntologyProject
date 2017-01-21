using System.Collections.Generic;
using System.Linq;
using MainModule.Models;
using MainModule.ViewModel;
using SparqlCaller;
using SparqlCaller.Common;
using SparqlCaller.Entities;

namespace MainModule.Managers
{
    public class QueryImplemantationManager
    {
        private IEnumerable<ItemViewModel> _movieList = new List<ItemViewModel>();

        public IEnumerable<ItemViewModel> MovieList => _movieList; 

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

            var allMovies =
                SparqlQueryBase.GetEntities<Movie>(Consts.URL.DbPedia, QueryBuilder.CreateQuery(Consts.URL.DbPedia,filterType)).ToList();

            var movieList = new List<ItemViewModel>();
            foreach (var movie in allMovies)
            {
                movieList.Add(new ItemViewModel(movie as Movie));
            }
            _movieList = movieList;
        }
    }
}