using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Models
{
    public class Booking
    {

        public int Id { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        [NotMapped]
        public List<int> SpecialRequestIds { get; set; } = new();

        public string GuestName { get; set; }

        public bool IsRecurring { get; set; }
    }
}
