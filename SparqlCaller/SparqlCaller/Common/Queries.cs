using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparqlCaller.Common
{
    public class Queries
    {
        public class Base
        {
            public const string AllGenres = @"PREFIX movie: <http://data.linkedmdb.org/resource/movie/>
                                                SELECT DISTINCT ?title WHERE
                                                    {
                                                        SERVICE <http://data.linkedmdb.org/sparql> { 
		                                        ?movie <http://data.linkedmdb.org/resource/movie/genre> ?genre.
    	                                        ?genre movie:film_genre_name ?title
                                                    }
                                                }";

            public const string AllCountries = @"PREFIX movie: <http://data.linkedmdb.org/resource/movie/> 
                                                SELECT DISTINCT ?label
                                                WHERE {
                                                SERVICE <http://data.linkedmdb.org/sparql> {
                                                        ?country a movie:country;
    	                                                 movie:country_name ?label.
                                                } }";
        }
        public class Templates
        {
            public const string MovieByTitle = @"PREFIX movie: <http://data.linkedmdb.org/resource/movie/>
                                                   PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                                                    SELECT ?title ?directorName ?countryName ?date ?imdb 
                                                    WHERE { 
                                                   SERVICE <http://data.linkedmdb.org/sparql> { 
                                                      ?film <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://data.linkedmdb.org/resource/movie/film> ;
                                                        <http://www.w3.org/2000/01/rdf-schema#label> ?title ; 
                                                        <http://purl.org/dc/terms/date> ?date ; 
                                                        <http://xmlns.com/foaf/0.1/page> ?imdb .
                                                OPTIONAL {?film movie:director ?dir.
                                                                ?dir movie:director_name ?directorName.}
                                                OPTIONAL {?film movie:country ?country.
                                                                ?country rdfs:label ?countryName.}
    	
                                                      FILTER(REGEX(?title, '{0}', 'i') && REGEX(STR(?imdb), 'www.imdb.com'))
                                              }}";
            public const string MovieByCity =   @"PREFIX movie: <http://data.linkedmdb.org/resource/movie/>
                                                    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                                                    PREFIX dc: <http://purl.org/dc/elements/1.1/>
                                                        SELECT ?title ?locationName
                                                            WHERE { 
                                                      SERVICE <http://data.linkedmdb.org/sparql> { 
                                                              ?movie movie:featured_film_location ?location.
    	                                                    ?movie rdfs:label ?title.
    	                                                    ?location movie:film_location_name ?locationName
                                                        FILTER(REGEX(?locationName, '{0}', 'i'))
                                                      }}";

            public const string RelatedBook = @"PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
                                                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> 
                                                PREFIX movie: <http://data.linkedmdb.org/resource/movie/> 
                                                PREFIX fts: <http://rdf.useekm.com/fts#> 
                                                PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> 
                                                SELECT * WHERE {
                                                SERVICE <http://data.linkedmdb.org/sparql>  
                                                { ?film a movie:film. 
                                                  ?film rdfs:label ?titlemovie. 
                                                  ?film <http://data.linkedmdb.org/resource/movie/actor> ?actor. 
                                                  ?film <http://data.linkedmdb.org/resource/movie/relatedBook> ?relatedBook 
                                                    FILTER ( Regex(?titlemovie,'{0}','i')) }}";

            public const string AllMoviesWithDbpediaLinks = @"PREFIX owl: <http://www.w3.org/2002/07/owl#>
                                                                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> 
                                                                PREFIX movie: <http://data.linkedmdb.org/resource/movie/> 
                                                                PREFIX dcterms: <http://purl.org/dc/terms/>
                                                                SELECT ?film ?label ?subject
                                                                WHERE {
                                                                SERVICE <http://data.linkedmdb.org/sparql> {
                                                                        ?film a movie:film .
                                                                        ?film rdfs:label ?label .
                                                                        ?film owl:sameAs ?dbpediaLink .
                                                                FILTER regex(STR(?dbpediaLink), 'dbpedia', 'i') }
                                                                SERVICE<http://dbpedia.org/sparql> { ?dbpediaLink dcterms:subject ?subject .
                                                                }
                                                                    }
                                                                    LIMIT 10";

            public const string AllMoviesWithOptionalFiltersBase = @"PREFIX owl: <http://www.w3.org/2002/07/owl#>
                                                                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> 
                                                                PREFIX dbo: <http://dbpedia.org/ontology/>
                                                                PREFIX foaf: <http://xmlns.com/foaf/0.1/>
                                                                PREFIX yago: <http://yago-knowledge.org/resource/>
                                                                PREFIX dbpediaowl: <http://dbpedia.org/ontology/>
                                                                PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
                                                                PREFIX movie: <http://data.linkedmdb.org/resource/movie/>
                                                                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                                                                PREFIX dc: <http://purl.org/dc/elements/1.1/>
                                                                    SELECT ?title ?genreName ?date ?writerName ?directorName ?countryName
                                                                        WHERE { 
                                                                  SERVICE <http://data.linkedmdb.org/sparql> { 
                                                                          ?movie rdf:type movie:film;
    		                                                                rdfs:label ?title.
                                                                            {0} }
                                                                ";

            public const string AllPrefixes = @"PREFIX owl: <http://www.w3.org/2002/07/owl#>
                                                                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> 
                                                                PREFIX dbo: <http://dbpedia.org/ontology/>
                                                                PREFIX foaf: <http://xmlns.com/foaf/0.1/>
                                                                PREFIX yago: <http://yago-knowledge.org/resource/>
                                                                PREFIX dbpediaowl: <http://dbpedia.org/ontology/>
                                                                PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
                                                                PREFIX movie: <http://data.linkedmdb.org/resource/movie/>
                                                                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                                                                PREFIX dc: <http://purl.org/dc/elements/1.1/>
                                                                PREFIX dbp: <http://dbpedia.org/property/>";

            public const string SelectBase = @"SELECT ?label ?genreName ?date ?writerName ?directorName ?countryName
                                                                        WHERE { SERVICE <http://data.linkedmdb.org/sparql> { 
                                                                          ?movie rdf:type movie:film;
    		                                                                rdfs:label ?label.";

            public const string SelectBaseDbPedia = @"SELECT ?label ?genreName ?date ?writerName ?directorName ?countryName ?duration ?budget
                                                                        WHERE { SERVICE <http://dbpedia.org/sparql> { 
                                                                          ?movie rdf:type dbo:Film;
    		                                                                rdfs:label ?label.
                                                                           FILTER langMatches( lang(?label), 'en' ).";
        }

        public class FilterSnippets
        {
            public const string GenreFilter = @"
                                                                      ?movie movie:genre ?genre.
     			                                                                ?genre movie:film_genre_name ?genreName.
                                                                       FILTER(REGEX(?genreName,'{0}','i'))
                                                                   ";

            public const string GenreFilterDbPedia = @"?movie dbp:genre ?genre.
     			                                                                ?genre rdfs:label ?genreName.
                                                                       FILTER(REGEX(?genreName,'{0}','i')).                                     
                                                                   ";

            public const string DateFilter = @"
                                                                        ?movie movie:initial_release_date ?date.
                                                                        FILTER(REGEX(?date,'{0}','i'))
                                                                        ";

            public const string DateFilterDbPedia = @"
                                                                         ?movie movie:initial_release_date ?date.
                                                                        FILTER(REGEX(?date,'{0}','i'))
                                                                        ";

            public const string WriterFilter = @"
    	                                                                ?movie movie:writer ?writer.
      	                                                                ?writer movie:writer_name ?writerName.
                                                                        FILTER(REGEX(?writerName,'{0}','i')).
                                                                    ";

            public const string WriterFilterDbPedia = @"
    	                                                                ?movie dbo:writer ?writer.
      	                                                                ?writer rdfs:label ?writerName.
                                                                        FILTER(REGEX(?writerName,'{0}','i')).
                                                                        FILTER langMatches( lang(?writerName), 'en' ).
                                                                    ";

            public const string DirectorFilter =
                                            @"
    	                                                                ?movie movie:director ?director.
      	                                                                ?director movie:director_name ?directorName.
                                                                        FILTER(REGEX(?directorName,'{0}','i'))
                                                                    ";

            public const string DirectorFilterDbPedia = @"
                                                                        ?movie dbo:director ?director.
      	                                                                ?director rdfs:label ?directorName.
                                                                        FILTER(REGEX(?directorName,'{0}','i')).
                                                                        FILTER langMatches( lang(?directorName), 'en' ).";

            public const string CountryFilter = @"
    	                                                                ?movie movie:country ?country.
      	                                                                ?country movie:country_name ?countryName.
                                                                        FILTER(REGEX(?country,'{0}','i'))
                                                                    ";

            public const string CountryFilterDbPedia = @"";


            public const string DurationFilterDbPedia = @"               ?movie <http://dbpedia.org/ontology/Work/runtime> ?duration.
                                                                            ";

            public const string BudgetFilterDbPedia = @"                ?movie dbo:budget ?budget.";
        }
    }
}
