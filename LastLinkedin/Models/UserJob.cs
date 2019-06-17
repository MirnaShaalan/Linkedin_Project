using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class UserJob
    {
        [Key, Column(Order = 0)]
        [StringLength(128)]
        public string UserID { get; set; }

        [Key, Column(Order = 1)]
        public int JobID { get; set; }


        public virtual Job Job { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}