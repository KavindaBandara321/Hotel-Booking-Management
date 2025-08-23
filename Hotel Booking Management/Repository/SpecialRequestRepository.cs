using Hotel_Booking_Management.IRepository;
using Hotel_Booking_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Repository
{
    public class SpecialRequestRepository : ISpecialRequestRepository
    {
        private readonly HotelDbContext _context;

        public SpecialRequestRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SpecialRequest>> GetAllAsync()
        {
            return await _context.SpecialRequests.ToListAsync();
        }

        public async Task<SpecialRequest> GetByIdAsync(int id)
        {
            return await _context.SpecialRequests.FindAsync(id);
        }

        public async Task<SpecialRequest> AddAsync(SpecialRequest specialRequest)
        {
            await _context.SpecialRequests.AddAsync(specialRequest);
            await _context.SaveChangesAsync();
            return specialRequest;
        }

        public async Task<SpecialRequest> UpdateAsync(SpecialRequest specialRequest)
        {
            _context.SpecialRequests.Update(specialRequest);
            await _context.SaveChangesAsync();
            return specialRequest;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SpecialRequests.FindAsync(id);
            if (entity == null)
                return false;

            _context.SpecialRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
