using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class PetBookingModel
    {
        [Key]
        public Guid PetBookingId { get; set; }
        [Required]
        public PetModel Pet { get; set; }
        [Required]
        public BookingModel Booking { get; set; }
        public PetBookingModel()
        {
            PetBookingId = Guid.NewGuid();
        }
    }
}