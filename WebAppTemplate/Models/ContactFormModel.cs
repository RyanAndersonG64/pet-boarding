using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppTemplate.Models
{
    public class ContactFormModel
    {
        [Key]
        public Guid ContactFormId { get; set; }
        [Required]
        public UserModel User { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Responded { get; set; } = false;
        public ContactFormModel()
        {
            ContactFormId = Guid.NewGuid();
        }

    }
}