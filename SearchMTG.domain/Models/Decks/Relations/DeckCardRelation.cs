using SearchMTG.domain.Cards.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchMTG.domain.Models.Decks.Relations
{
    public class DeckCardRelation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual Deck Deck { get; set; }
        [Required]
        public virtual CardInfo Card { get; set; }
    }
}
