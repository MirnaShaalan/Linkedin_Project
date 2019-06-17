using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LastLinkedin.Models
{
    public class Group
    {
        public int Id { get; set; }
        //this group created by member this is his id
        [StringLength(128)]
        public string Admin_Id { get; set; }
        

        [Required(ErrorMessage = ("Plz enter group name!!!"))]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? lastActivity { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<GMessage> GMessages { get; set; }
        /////////other deatails///////////////////////////////

    }
}