﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        [StringLength(128)]
        public string PublishedID { get; set; }
        [Required]
        public String Description { get; set; }

        public virtual Post Post { get; set; }
    }
}