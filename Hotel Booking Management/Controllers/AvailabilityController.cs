using Hotel_Booking_Management.DataManage;
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
    { // GET: api/Availability/predict?from=2025-07-14&to=2025-07-20
        [HttpGet("predict")]
        public IActionResult PredictAvailability([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            int totalRooms = DataStore.Rooms.Count;
            var predictions = new List<object>();

            for (DateTime date = from.Date; date <= to.Date; date = date.AddDays(1))
            {
                int bookedCount = DataStore.Bookings
                    .Count(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date);

                double occupancyRate = totalRooms == 0 ? 0 : (double)bookedCount / totalRooms;
                string trend = occupancyRate switch
                {
                    < 0.3 => "Low",
                    < 0.7 => "Moderate",
                    _ => "High"
                };

                decimal averagePrice = DataStore.Rooms.Select(r => r.PricePerNight).DefaultIfEmpty(0).Average();
                decimal predictedPrice = averagePrice * (decimal)(1.0 + (occupancyRate * 0.25)); // +25% at full occupancy

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
