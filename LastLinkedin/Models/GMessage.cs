using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class GMessage
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [StringLength(128)]
        public string ReceiverId { get; set; }
        [Required]
        public string Msg { get; set; }

        public string Date { get; set; }
        [Range(0, 1)]
        public int Read { get; set; }

        public virtual Group Group { get; set; }
    }
}