﻿using System.Collections.Generic;
using System;
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
            //if (queryParameters.IsDateChecked)
            //    filterType |= FilterType.Date;
            if (queryParameters.IsDirectorChecked)
                filterType |= FilterType.Director;
            if (queryParameters.IsGenreChecked)
                filterType |= FilterType.Genre;
            if (queryParameters.IsWriterChecked)
                filterType |= FilterType.Writer;

            var allMovies =
                SparqlQueryBase.GetEntities<Movie>(Consts.URL.DbPedia
                    , QueryBuilder.CreateQuery(Consts.URL.DbPedia
                        , filterType
                        , queryParameters.RowLimit
                        , queryParameters.Country
                        , queryParameters.DirectorName
                        , queryParameters.Date
                        , queryParameters.WriterName
                        , queryParameters.Genre))
                        .Where(ent => (!queryParameters.IsDateChecked || (ent as Movie).DateTime.HasValue && (ent as Movie).DateTime.Value.Date.Year == DateTime.Parse(queryParameters.Date).Date.Year))
                        .ToList();
            
            var movieList = new List<ItemViewModel>();
            int index = 0;
            foreach (var movie in allMovies)
            {
                if ((!queryParameters.RowLimit.HasValue || index++ < queryParameters.RowLimit.Value ) && !string.IsNullOrEmpty((movie as Movie).Title))
                    movieList.Add(new ItemViewModel(movie as Movie));
                else break;
            }
            _movieList = movieList;
        }
    }
}