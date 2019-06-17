using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Recommendation
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(128)]
        public string User_recommending_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(128)]
        public string User_being_recommended_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_of_recommendation { get; set; }

        public string other_details { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser User1 { get; set; }
    }
}