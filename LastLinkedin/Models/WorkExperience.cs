using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class WorkExperience
    {
        public int ID { get; set; }
        
        [Required]
        public String Title { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<UserWorkExperience> UserWorkExperiences { get; set; }

        //public int ID { get; set; }
        //[Required]
        //public String Title { get; set; }
        //[Required]
        //public string CompanyName { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime StartDate { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime? EndDate { get; set; }

        //public virtual ICollection<UserWorkExperience> UserWorkExperiences { get; set; }




    }
}