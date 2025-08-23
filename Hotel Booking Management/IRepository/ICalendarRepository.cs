using Hotel_Booking_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.IRepository
{
    public interface ICalendarRepository
    {
        Task<List<Booking>> GetBookingsByDateAsync(DateTime date);
        Task<Room> GetRoomByIdAsync(int roomId);
        Task<List<SpecialRequest>> GetSpecialRequestsByIdsAsync(IEnumerable<int> ids);
    }
}
