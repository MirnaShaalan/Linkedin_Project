using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Message
    {
        public int Id { get; set; }
        [StringLength(128)]
        [Required]
        public string UserId { get; set; }
        [StringLength(128)]
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        public string Msg { get; set; }

        public string Date { get; set; }
        [Range(0, 1)]
        public int Read { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}