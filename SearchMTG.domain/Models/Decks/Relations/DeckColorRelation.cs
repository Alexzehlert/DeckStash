using SearchMTG.domain.Cards.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchMTG.domain.Models.Decks.Relations
{
    public class DeckColorRelation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual Deck Deck { get; set; }
        [Required]
        public virtual Color Color { get; set; }
    }
}
