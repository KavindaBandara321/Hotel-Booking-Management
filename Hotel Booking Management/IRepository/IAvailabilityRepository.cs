using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.IRepository
{
    public interface IAvailabilityRepository
    {
        Task<int> GetTotalRoomsAsync();
        Task<int> GetBookedCountAsync(DateTime date);
        Task<decimal> GetAveragePriceAsync();
    }
}
