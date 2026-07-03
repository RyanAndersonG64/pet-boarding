using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBoarding.Models
{
    public class BookingModel
    {
        [Key]
        public Guid BookingId { get; set; }
        public UserModel User { get; set; }
        public DateTime ScheduledCheckIn { get; set; }
        public DateTime? ActualCheckIn { get; set; }
        public EmployeeModel CheckedInBy { get; set; }
        public DateTime ScheduledCheckOut { get; set; }
        public DateTime? ActualCheckOut { get; set; }
        public EmployeeModel CheckedOutBy { get; set; }
        public string Status { get; set; } = "Upcoming";
        public BookingModel() 
        {
            BookingId = Guid.NewGuid();
        }
    }
}