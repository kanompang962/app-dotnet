using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_dotnet.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int pageNumber { get; set; } = 0;
        public int pageSize { get; set; } = 0;
    }
}