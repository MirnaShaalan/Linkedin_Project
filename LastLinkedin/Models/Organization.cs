using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LastLinkedin.Models
{
    public class Organization
    {
        public int ID { get; set; }

        [Required(ErrorMessage = ("Plz enter your organization name!!!"))]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Organization_Description { get; set; }

        //public string Other_Details { get; set; }

        //To be modified when members class willbe added
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<OrganizationJob> OrganizationJobs { get; set; }




    }
}