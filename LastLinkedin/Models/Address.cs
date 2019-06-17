using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Address
    {
        public int ID { get; set; }

        public string Line_1 { get; set; }
        public string Line_2 { get; set; }
        public string Line_3 { get; set; }

        public string city { get; set; }
        public string State_County_Province { get; set; }
        public string Zip_or_Postcode { get; set; }
        public string Country { get; set; }
        //public string Other_Details { get; set; }

        //public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }


    }
}