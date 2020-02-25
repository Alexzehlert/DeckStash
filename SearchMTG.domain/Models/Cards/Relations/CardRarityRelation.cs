using SearchMTG.domain.Cards.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchMTG.domain.Models.Cards.Relations
{
    public class CardRarityRelation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual CardInfo Card { get; set; }
        [Required]
        public virtual CardRarity CardRarity { get; set; }
    }
}
