using SearchMTG.domain.Models.Users.Relations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchMTG.domain.Models.Users
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(64)]
        public int Guid { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(30)]
        public string SiteName { get; set; }
        [StringLength(40)]
        public string FirstName { get; set; }
        [StringLength(40)]
        public string LastName { get; set; }

        // Relations
        public virtual ICollection<UserDeckRelation> Decks { get; set; }
    }
}
