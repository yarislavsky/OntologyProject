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
                var allMovies = SparqlQueryBase.GetEntities<Movie>(Consts.URL.DbPedia, QueryBuilder.CreateQuery(Consts.URL.DbPedia, FilterType.Date,dateFilter:"1992"));

                Console.WriteLine("Title\tSubject\tLink\tGenre\tDate\tDirector\tWriter");
                foreach (Movie movie in allMovies)
                {
                    Console.WriteLine($"{movie.Title}\t{movie.Subject}\t{movie.Link}\t{movie.Genre}\t{movie.DateTime}\t{movie.Director}\t{movie.Writer}");
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
