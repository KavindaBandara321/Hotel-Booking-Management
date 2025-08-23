using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Dtos
{
    public class ReportEntryDto
    {
        public int BookingId { get; set; }
        public string GuestName { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public List<string> SpecialRequests { get; set; }

    }
}
