using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomType { get; set; } // e.g., Deluxe, Suite
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
