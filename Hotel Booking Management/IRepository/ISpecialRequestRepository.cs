using Hotel_Booking_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.IRepository
{
    public interface ISpecialRequestRepository
    {
        Task<IEnumerable<SpecialRequest>> GetAllAsync();
        Task<SpecialRequest> GetByIdAsync(int id);
        Task<SpecialRequest> AddAsync(SpecialRequest specialRequest);
        Task<SpecialRequest> UpdateAsync(SpecialRequest specialRequest);
        Task<bool> DeleteAsync(int id);
    }
}
