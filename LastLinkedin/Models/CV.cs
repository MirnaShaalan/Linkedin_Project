using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LastLinkedin.Models
{
    public class CV
    {
        //public CV()
        //{
        //   // CVSections = new HashSet<CVSection>();
        //}

        public int ID { get; set; }

        //[ForeignKey("Member")]
        //public int Member_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime date_created { get; set; }

        [DataType(DataType.Date)]
        public DateTime date_updated { get; set; }

        //  public virtual ICollection<CVSection> CVSections { get; set; }

        //[StringLength(1000)]

        [DataType(DataType.Upload)]
        public string cvPath { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}