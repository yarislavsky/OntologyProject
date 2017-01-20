namespace MainModule.Models
{
    public class QueryParameters
    {
        public bool IsGenreChecked { get; set; }

        public bool IsDateChecked { get; set; }

        public bool IsWriterChecked { get; set; }

        public bool IsDirectorChecked { get; set; }

        public bool IsCountryChecked { get; set; }

        public string WriterName { get; set; }

        public string DirectorName { get; set; }
    }
}