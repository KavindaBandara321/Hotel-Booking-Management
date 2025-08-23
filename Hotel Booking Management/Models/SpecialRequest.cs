using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Models
{
    public class SpecialRequest
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
