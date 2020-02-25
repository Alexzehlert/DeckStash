using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchMTG.domain.ViewModels
{
    public class CardResponseViewModel
    {
        public CMCViewModel CMCs { get; set; }
        public IEnumerable<CardViewModel> Cards { get; set; }
    }
}
