using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Models
{
    public class ReportEntry
    {
        [Key]
        public int BookingId { get; set; }
        public string GuestName { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        [NotMapped]
        public List<string> SpecialRequests { get; set; }
    }
}
