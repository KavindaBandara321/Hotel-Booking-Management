using Hotel_Booking_Management.DataManage;
using Hotel_Booking_Management.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        //[HttpGet("predict")]
        //public IActionResult PredictAvailability([FromQuery] DateTime from, [FromQuery] DateTime to)
        //{
        //    int totalRooms = DataStore.Rooms.Count;
        //    var predictions = new List<object>();

        //    for (DateTime date = from.Date; date <= to.Date; date = date.AddDays(1))
        //    {
        //        int bookedCount = DataStore.Bookings
        //            .Count(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date);

        //        double occupancyRate = totalRooms == 0 ? 0 : (double)bookedCount / totalRooms;
        //        string trend = occupancyRate switch
        //        {
        //            < 0.3 => "Low",
        //            < 0.7 => "Moderate",
        //            _ => "High"
        //        };

        //        decimal averagePrice = DataStore.Rooms.Select(r => r.PricePerNight).DefaultIfEmpty(0).Average();
        //        decimal predictedPrice = averagePrice * (decimal)(1.0 + (occupancyRate * 0.25));

        //        predictions.Add(new
        //        {
        //            Date = date.ToShortDateString(),
        //            OccupancyRate = $"{occupancyRate:P0}",
        //            Demand = trend,
        //            PredictedPrice = $"{predictedPrice:C}"
        //        });
        //    }

        //    return Ok(predictions);
        //}
        private readonly IAvailabilityRepository _availabilityRepo;

        public AvailabilityController(IAvailabilityRepository availabilityRepo)
        {
            _availabilityRepo = availabilityRepo;
        }

        [HttpGet("predict")]
        public async Task<IActionResult> PredictAvailability([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            int totalRooms = await _availabilityRepo.GetTotalRoomsAsync();
            var predictions = new List<object>();

            for (DateTime date = from.Date; date <= to.Date; date = date.AddDays(1))
            {
                int bookedCount = await _availabilityRepo.GetBookedCountAsync(date);

                double occupancyRate = totalRooms == 0 ? 0 : (double)bookedCount / totalRooms;
                string trend = occupancyRate switch
                {
                    < 0.3 => "Low",
                    < 0.7 => "Moderate",
                    _ => "High"
                };

                decimal averagePrice = await _availabilityRepo.GetAveragePriceAsync();
                decimal predictedPrice = averagePrice * (decimal)(1.0 + (occupancyRate * 0.25));

                predictions.Add(new
                {
                    Date = date.ToShortDateString(),
                    OccupancyRate = $"{occupancyRate:P0}",
                    Demand = trend,
                    PredictedPrice = $"{predictedPrice:C}"
                });
            }

            return Ok(predictions);
        }
    }
}
