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
    public class CalendarController : ControllerBase
    {
        // GET: api/Calendar/weekly?startDate=2025-07-14
        //[HttpGet("weekly")]
        //public IActionResult GetWeeklyCalendar([FromQuery] DateTime startDate)
        //{
        //    var calendar = Enumerable.Range(0, 7)
        //        .Select(offset => startDate.Date.AddDays(offset))
        //        .ToDictionary(
        //            date => date.ToString("dddd"),
        //            date => DataStore.Bookings
        //                .Where(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date)
        //                .Select(b => new
        //                {
        //                    BookingId = b.Id,
        //                    GuestName = b.GuestName,
        //                    Room = DataStore.Rooms.FirstOrDefault(r => r.Id == b.RoomId)?.RoomType ?? "Unknown",
        //                    SpecialRequests = b.SpecialRequestIds
        //                        .Select(id => DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id)?.Description)
        //                        .Where(desc => !string.IsNullOrEmpty(desc))
        //                        .ToList()
        //                }).ToList()
        //        );

        //    return Ok(calendar);
        //}

        private readonly ICalendarRepository _calendarRepo;

        public CalendarController(ICalendarRepository calendarRepo)
        {
            _calendarRepo = calendarRepo;
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyCalendar([FromQuery] DateTime startDate)
        {
            var calendar = new Dictionary<string, List<object>>();

            for (int offset = 0; offset < 7; offset++)
            {
                var date = startDate.Date.AddDays(offset);
                var bookings = await _calendarRepo.GetBookingsByDateAsync(date);

                var dayBookings = new List<object>();

                foreach (var booking in bookings)
                {
                    var room = await _calendarRepo.GetRoomByIdAsync(booking.RoomId);
                    var specialRequests = await _calendarRepo.GetSpecialRequestsByIdsAsync(booking.SpecialRequestIds);

                    dayBookings.Add(new
                    {
                        BookingId = booking.Id,
                        GuestName = booking.GuestName,
                        Room = room?.RoomType ?? "Unknown",
                        SpecialRequests = specialRequests.Select(r => r.Description).ToList()
                    });
                }

                calendar[date.ToString("dddd")] = dayBookings;
            }

            return Ok(calendar);
        }
    }
}
