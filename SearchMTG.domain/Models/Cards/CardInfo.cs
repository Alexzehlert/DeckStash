using SearchMTG.domain.Models.Cards.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SearchMTG.domain.Cards.Models
{
    public class CardInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string CardId { get; set; }
        public DateTime ReleaseDate { get; set; }
        [StringLength(60)]
        public string Name { get; set; }
        [StringLength(60)]
        public string ManaCost { get; set; }
        public float ConvertedManaCost { get; set; }
        [StringLength(60)]
        public string TypeName { get; set; }
        [StringLength(800)]
        public string Text { get; set; }
        [StringLength(700)]
        public string Flavor { get; set; }
        [StringLength(10)]
        public string Power { get; set; }
        [StringLength(10)]
        public string Toughness { get; set; }
        public double Price { get; set; }
        [StringLength(50)]
        public string Artist { get; set; }
        [StringLength(100)]
        public string NormalImage { get; set; }
        [StringLength(150)]
        public string CroppedImage { get; set; }

        // Relations
        public virtual ICollection<CardRarityRelation> CardRarity { get; set; }
        public virtual ICollection<CardSetRelation> CardSet { get; set; }
        public virtual ICollection<CardTypeRelation> Types { get; set; }
        public virtual ICollection<CardSubTypeRelation> SubTypes { get; set; }
        public virtual ICollection<CardColorRelation> Colors { get; set; }
    }
}
