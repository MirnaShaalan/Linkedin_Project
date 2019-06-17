﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Education
    {
        public int ID { get; set; }
        [Required]
        public String Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int Grade { get; set; }

        public virtual ICollection<UserEducation> UserEducations { get; set; }

    }
}