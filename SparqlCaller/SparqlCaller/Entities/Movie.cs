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
        public string Genre { get; set; }
        public DateTime? DateTime { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string ImgUrl { get; set; }

        public IEntity FillEntity(SparqlResult sResult)
        {
            Title = sResult.GetValue("label").RemoveLanguageLabel();
            Link = sResult.GetValue("film");
            Subject = sResult.GetValue("subject");

            Genre = sResult.GetValue("genreName");
            DateTime = ConversionUtil.ConvertToDateTime(sResult.GetValue("date"));
            Director = sResult.GetValue("directorName");
            Writer = sResult.GetValue("writerName");

            // TODO get img url from response
            ImgUrl = string.Empty;
            
            return this;
        }
    }
}
