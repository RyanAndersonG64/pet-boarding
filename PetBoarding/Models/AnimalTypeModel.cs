using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class AnimalTypeModel
    {
        [Key]
        public Guid AnimalTypeId { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; } = 0;
        public List<PetModel> Pets { get; set; }
        public AnimalTypeModel()
        {
            AnimalTypeId = Guid.NewGuid();
        }
    }
}