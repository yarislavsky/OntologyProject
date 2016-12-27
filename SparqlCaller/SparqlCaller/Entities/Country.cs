using VDS.RDF.Query;

namespace SparqlCaller.Entities
{
    public class Country:IEntity
    {


        public IEntity FillEntity(SparqlResult sResult)
        {
            return this;
        }
    }
}