using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Job
    {
        public int ID { get; set; }
       
        [Required]
        public String Title { get; set; }

        public String Description { get; set; }



        public virtual ICollection<UserJob> UserJobs { get; set; }
        public virtual ICollection<OrganizationJob> OrganizationJobs { get; set; }


    }
}