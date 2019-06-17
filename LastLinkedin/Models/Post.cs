using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LastLinkedin.Models
{
    public class Post
    {
        public int ID { get; set; }
        [Required]
        public String Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedPostDate { get; set; }

        [Range(0,1)]
        public int ReadPost { get; set; }
		
		[Range(0, 1)]
        public int Liked { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }   //published id

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }




    }
}