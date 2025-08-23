using Hotel_Booking_Management.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Repository
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly HotelDbContext _context;

        public AvailabilityRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalRoomsAsync()
        {
            return await _context.Rooms.CountAsync();
        }

        public async Task<int> GetBookedCountAsync(DateTime date)
        {
            return await _context.Bookings
                .CountAsync(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date);
        }

        public async Task<decimal> GetAveragePriceAsync()
        {
            return await _context.Rooms
                .Select(r => r.PricePerNight)
                .DefaultIfEmpty(0)
                .AverageAsync();
        }
    }
}
