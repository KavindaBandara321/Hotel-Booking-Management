using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Dtos
{
    public class SpecialRequestDto
    {
        public int Id { get; set; }
        public string Description { get; set; } // e.g., "Late Check-In"
    }
}
