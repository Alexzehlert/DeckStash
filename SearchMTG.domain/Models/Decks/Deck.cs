using SearchMTG.domain.Models.Decks.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchMTG.domain.Models.Decks
{
    public class Deck
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(3000)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        

        // Relations
        public virtual ICollection<DeckCardRelation> Cards { get; set; }
        public virtual ICollection<DeckColorRelation> Colors { get; set; }
        public virtual ICollection<DeckVoteRelation> Votes { get; set; }
    }
}
