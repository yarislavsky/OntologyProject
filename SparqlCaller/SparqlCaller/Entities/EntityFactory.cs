using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Query;

namespace SparqlCaller.Entities
{
    public static class EntityFactory
    {
        public static IEntity GetEntity<T>(SparqlResult sResult) where T : IEntity
        {
            if (typeof(T) == typeof(Movie))
            {
                return (new Movie()).FillEntity(sResult);
            }

            if (typeof(T) == typeof(Book))
            {
                return (new Book()).FillEntity(sResult);
            }

            if (typeof(T) == typeof(City))
            {
                return (new City()).FillEntity(sResult);
            }
            return null;
        }
    }
}
