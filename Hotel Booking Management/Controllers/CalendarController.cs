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
    public class CalendarController : ControllerBase
    {
        // GET: api/Calendar/weekly?startDate=2025-07-14
        [HttpGet("weekly")]
        public IActionResult GetWeeklyCalendar([FromQuery] DateTime startDate)
        {
            var calendar = Enumerable.Range(0, 7)
                .Select(offset => startDate.Date.AddDays(offset))
                .ToDictionary(
                    date => date.ToString("dddd"),
                    date => DataStore.Bookings
                        .Where(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date)
                        .Select(b => new
                        {
                            BookingId = b.Id,
                            GuestName = b.GuestName,
                            Room = DataStore.Rooms.FirstOrDefault(r => r.Id == b.RoomId)?.RoomType ?? "Unknown",
                            SpecialRequests = b.SpecialRequestIds
                                .Select(id => DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id)?.Description)
                                .Where(desc => !string.IsNullOrEmpty(desc))
                                .ToList()
                        }).ToList()
                );

            return Ok(calendar);
        }
    }
}
