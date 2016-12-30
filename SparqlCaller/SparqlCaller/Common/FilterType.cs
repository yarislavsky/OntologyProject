using System;

namespace SparqlCaller.Common
{
    [Flags]
    public enum FilterType
    {
        Genre = 1,
        Date = 1<<1,
        Writer = 1<<2,
        Director = 1<<3,
        Country = 1<<4,
        All = Genre | Date | Writer | Director | Country,
    }
}
