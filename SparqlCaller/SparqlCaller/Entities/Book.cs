using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Query;

namespace SparqlCaller.Entities
{
    public class Book : IEntity
    {

        public string Title { get; set; }

        public IEntity FillEntity(SparqlResult sResult)
        {
            return this;
        }
    }
}
