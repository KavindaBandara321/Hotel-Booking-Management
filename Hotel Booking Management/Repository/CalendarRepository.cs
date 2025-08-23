using Hotel_Booking_Management.IRepository;
using Hotel_Booking_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Repository
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly HotelDbContext _context;

        public CalendarRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            return await _context.Bookings
                .Where(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date)
                .ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }

        public async Task<List<SpecialRequest>> GetSpecialRequestsByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.SpecialRequests
                .Where(r => ids.Contains(r.Id))
                .ToListAsync();
        }
    
    }
}
