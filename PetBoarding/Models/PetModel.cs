using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class PetModel
    {
        [Key]
        public Guid PetId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public AnimalTypeModel AnimalType { get; set; }
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        [MaxLength(1000)]
        public string SpecialInstructions { get; set; } = string.Empty;
        [Required]
        public EmergencyContactModel EmergencyContact { get; set; }
        public List<PetOwnerModel> Owners { get; set; }
        public PetModel ()
        {
            PetId = Guid.NewGuid();
        }
    }
}