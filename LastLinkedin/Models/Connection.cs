using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Connection
    {

        public int Connectionid { get; set; }
        [StringLength(128)]
        public string connection_user_id { get; set; }
        [StringLength(128)]
        public string user_id { get; set; }


        public bool? Accepted { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_connection_made { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationUser User1 { get; set; }

    }
}