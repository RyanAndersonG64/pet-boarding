using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class EmergencyContactModel
    {
        [Key]
        public Guid EmergencyContactId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        public List <PetModel> Pets { get; set; }
        public EmergencyContactModel()
        {
            EmergencyContactId = Guid.NewGuid();
        }
    }
}