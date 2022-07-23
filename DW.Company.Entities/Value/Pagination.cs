
using System.Collections.Generic;

namespace DW.Company.Entities.Value
{
    public class Pagination
    {
        public int Size { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }

        public int? Page { get; set; }
    }

    public class Pagination<T>
    {
        public int Size { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public int? Page { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
