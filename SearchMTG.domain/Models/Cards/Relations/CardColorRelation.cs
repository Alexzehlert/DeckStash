using Newtonsoft.Json;
using SearchMTG.domain.Cards.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SearchMTG.domain.Models.Cards.Relations
{
    public class CardColorRelation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual CardInfo Card { get; set; }
        [Required]
        public virtual Color Color { get; set; }
    }
}
