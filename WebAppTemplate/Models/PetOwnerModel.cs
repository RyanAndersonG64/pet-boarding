using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAppTemplate.Models
{
    public class PetOwnerModel
    {
        [Key]
        public Guid PetOwnerId { get; set; }
        [Required]
        public UserModel User { get; set; }
        [Required]
        public PetModel Pet { get; set; }
        public PetOwnerModel()
        {
            PetOwnerId = Guid.NewGuid();
        }
    }
}