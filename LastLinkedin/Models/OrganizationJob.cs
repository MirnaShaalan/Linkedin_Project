using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class OrganizationJob
    {
        [Key, Column(Order = 0)]
        [StringLength(128)]
        public string UserID { get; set; }

        [Key, Column(Order = 1)]
        public int OrganizationID { get; set; }

       


        public virtual Job Job { get; set; }
        public virtual Organization Organization { get; set; }
    }
}