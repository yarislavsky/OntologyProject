using SparqlCaller.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Query;

namespace SparqlCaller.Entities
{
    public class Movie : IEntity
    {

        public string Title { get; set; }
        public string Link { get; set; }
        public string Subject { get; set; }
        public IEntity FillEntity(SparqlResult sResult)
        {
            Title = sResult.GetValue("label");
            Link = sResult.GetValue("film");
            Subject = sResult.GetValue("subject");

            return this;
        }
    }
}
