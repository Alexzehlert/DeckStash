using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchMTG.domain.ViewModels
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rarity { get; set; }
        public int SetName { get; set; }
        public string ManaCost { get; set; }
        public float CMC { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public string Artist { get; set; }
        public string NormalImage { get; set; }
        public string CroppedImage { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
    }
}
