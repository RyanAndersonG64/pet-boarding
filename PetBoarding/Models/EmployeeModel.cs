using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class EmployeeModel
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public bool AdminStatus { get; set; } = false;
        public EmployeeModel ()
        {
            EmployeeId = Guid.NewGuid();
        }
    }
}