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
            {"Action", "action"},
            {"Adventure", "adventur"},
            {"Comedy", "comed"},
            {"Crime", "crime"},
            {"Drama", "drama"},
            {"Fantasy", "fantas"},
            {"Historical", "histor"},
            {"Horror", "horror"},
            {"Musical", "music"},
            {"Science fiction", "fiction"},
            {"Thriller", "thril"},
            {"Western", "western"},
        };
    }
}