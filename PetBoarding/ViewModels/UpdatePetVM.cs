using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetBoarding.Models;

namespace PetBoarding.ViewModels
{


    public class UpdatePetVM
    {
        public PetModel Pet { get; set; }
        public AnimalTypeModel AnimalType { get; set;}
        public EmergencyContactModel EmergencyContact { get; set;}
        public List<AnimalTypeModel> AnimalTypes { get; set; }
        public List<EmergencyContactModel> EmergencyContacts { get; set; }
    }
}