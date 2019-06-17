using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class UserEducation
    {
        [Key, Column(Order = 0)]
        [StringLength(128)]
        public string UserID { get; set; }

        [Key, Column(Order = 1)]
        public int EducationID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Education Education { get; set; }

    }
}