using System.Collections.Generic;

namespace MainModule
{
    public static class Defaults
    {
        public const string DefaultImage = @"..\Resource\NotFound.jpg";

        public static string[] AllRowLimits => new []
        {
            "10",
            "50",
            "100",
            "All"
        };


        public static readonly Dictionary<string, string> AllGenres = new Dictionary<string, string>
        {
            // TODO replace values with to dbpedia genres
            {"Action", ""},
            {"Adventure", ""},
            {"Comedy", ""},
            {"Crime", ""},
            {"Drama", ""},
            {"Fantasy", ""},
            {"Historical", ""},
            {"Horror", ""},
            {"Science fiction", ""},
            {"Thriller", ""},
            {"Western", ""},
        };
    }
}