using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PetBoarding.IdentityModels;

namespace PetBoarding.Models
{
    public class PetOwnerModel
    {
        [Key]
        public Guid PetOwnerId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public PetModel Pet { get; set; }
        public PetOwnerModel()
        {
            PetOwnerId = Guid.NewGuid();
        }
    }
}