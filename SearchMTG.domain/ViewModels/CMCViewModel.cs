using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchMTG.domain.ViewModels
{
    // Converted Mana Cost
    public class CMCViewModel
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public List<CMCRangeViewModel> Ranges { get; set; }
    }

    public class CMCRangeViewModel
    {
        public int Cost { get; set; }
        public int Count { get; set; }
    }
}
