using Hotel_Booking_Management.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.DataManage
{
    public static class DataStore
    {
        public static List<BookingDto> Bookings { get; } = new();
        public static List<RoomDto> Rooms { get; } = new();
        public static List<SpecialRequestDto> SpecialRequests { get; } = new();
    }
}
