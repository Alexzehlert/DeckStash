using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchMTG.domain.Models.Query
{
    public class Query
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string InputSearch { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> SubTypeIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> RarityIds { get; set; }
        public List<int> SetIds { get; set; }
        public List<int> CmcRange { get; set; }
    }
}
