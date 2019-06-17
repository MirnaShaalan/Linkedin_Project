using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Skill
    {
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        [Key, Column(Order = 1)]
        [StringLength(128)]
        public string UserId { get; set; }
        [Required]
        public String Description { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}