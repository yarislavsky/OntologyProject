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
        public double Duration { get; set; }
        public double Budget { get; set; }

        public IEntity FillEntity(SparqlResult sResult)
        {
            if (!sResult.GetValue("genreName").IsEnglish())
                return null;
            //if (string.IsNullOrEmpty(sResult.GetValue("pictureLink")))
            //    return null;

            Title = sResult.GetValue("label").RemoveLanguageLabel();
            Link = sResult.GetValue("film");
            Subject = sResult.GetValue("subject");

            Genre = sResult.GetValue("genreName").RemoveLanguageLabel();
            DateTime = ConversionUtil.ConvertToDateTime(sResult.GetValue("date"));
            Director = sResult.GetValue("directorName").RemoveLanguageLabel();
            Writer = sResult.GetValue("writerName").RemoveLanguageLabel();
            Duration = ConversionUtil.ConvertToDouble(sResult.GetValue("duration"));
            Budget = ConversionUtil.ConvertToDouble(sResult.GetValue("budget"));
            // TODO get img url from response
            ImgUrl = sResult.GetValue("pictureLink");

            return this;
        }
    }
}
