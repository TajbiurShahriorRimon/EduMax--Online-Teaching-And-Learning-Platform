using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Admin
    {
        [ForeignKey("Credential")]
        public int AdminId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual Credential Credential { get; set; }
    }
}