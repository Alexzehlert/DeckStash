using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchMTG.domain.Models.Tracking
{
    public class UuidLogTimeStamp
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual UuidLog UuidLog { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
