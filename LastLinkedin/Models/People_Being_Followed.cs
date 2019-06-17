using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class People_Being_Followed
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(128)]
        public string UserId { get; set; }

        
        [Column(Order = 1)]
        [StringLength(128)]
        public string User_being_followed_id { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime date_start_following { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_stopped_following { get; set; }


        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser User1 { get; set; }
    }
}