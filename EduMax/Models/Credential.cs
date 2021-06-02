using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduMax.Models
{
    public class Credential
    {
        public int CredentialId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
    }
}