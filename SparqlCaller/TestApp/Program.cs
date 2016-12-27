using SparqlCaller;
using SparqlCaller.Common;
using SparqlCaller.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var allMovies = SparqlQueryBase.GetEntities<Movie>(Consts.URL.DbPedia, Queries.Templates.AllMoviesWithDbpediaLinks);


                foreach(Movie movie in allMovies)
                {
                    Console.WriteLine($"{movie.Title}\t{movie.Subject}\t{movie.Link}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
