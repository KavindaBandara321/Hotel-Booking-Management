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
    public class ReportsController : ControllerBase
    {

        //[HttpGet("weekly")]
        //public IActionResult GetWeeklyReport([FromQuery] DateTime startDate)
        //{
        //    var endDate = startDate.AddDays(6);
        //    var report = Enumerable.Range(0, 7)
        //        .Select(offset => startDate.Date.AddDays(offset))
        //        .ToDictionary(
        //            date => date.ToString("dddd"),
        //            date => DataStore.Bookings
        //                .Where(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date)
        //                .Select(b => new
        //                {
        //                    BookingId = b.Id,
        //                    b.GuestName,
        //                    b.RoomId,
        //                    SpecialRequests = b.SpecialRequestIds
        //                        .Select(id => DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id)?.Description)
        //                        .Where(desc => !string.IsNullOrEmpty(desc))
        //                        .ToList()
        //                }).ToList()
        //        );

        //    return Ok(report);
        //}


        //[HttpGet("monthly")]
        //public IActionResult GetMonthlyReport([FromQuery] DateTime startDate)
        //{
        //    var endDate = startDate.AddMonths(1).AddDays(-1); // end of the month

        //    var report = Enumerable.Range(0, (endDate - startDate).Days + 1)
        //        .Select(offset => startDate.Date.AddDays(offset))
        //        .ToDictionary(
        //            date => date.ToString("yyyy-MM-dd"),
        //            date => DataStore.Bookings
        //                .Where(b => b.CheckInDate.Date <= date && b.CheckOutDate.Date >= date)
        //                .Select(b => new
        //                {
        //                    BookingId = b.Id,
        //                    b.GuestName,
        //                    b.RoomId,
        //                    SpecialRequests = b.SpecialRequestIds
        //                        .Select(id => DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id)?.Description)
        //                        .Where(desc => !string.IsNullOrEmpty(desc))
        //                        .ToList()
        //                }).ToList()
        //        );

        //    return Ok(report);
        //}
        private readonly IReportRepository _reportRepo;

        public ReportsController(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyReport([FromQuery] DateTime startDate)
        {
            var calendar = new Dictionary<string, List<object>>();

            for (int offset = 0; offset < 7; offset++)
            {
                var date = startDate.Date.AddDays(offset);
                var bookings = await _reportRepo.GetBookingsByDateAsync(date);

                var dayBookings = new List<object>();
                foreach (var booking in bookings)
                {
                    var specialRequests = await _reportRepo.GetSpecialRequestsByIdsAsync(booking.SpecialRequestIds);

                    dayBookings.Add(new
                    {
                        booking.Id,
                        booking.GuestName,
                        booking.RoomId,
                        SpecialRequests = specialRequests.Select(r => r.Description).ToList()
                    });
                }

                calendar[date.ToString("dddd")] = dayBookings;
            }

            return Ok(calendar);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] DateTime startDate)
        {
            var endDate = startDate.AddMonths(1).AddDays(-1);
            int daysCount = (endDate - startDate).Days + 1;
            var calendar = new Dictionary<string, List<object>>();

            for (int offset = 0; offset < daysCount; offset++)
            {
                var date = startDate.Date.AddDays(offset);
                var bookings = await _reportRepo.GetBookingsByDateAsync(date);

                var dayBookings = new List<object>();
                foreach (var booking in bookings)
                {
                    var specialRequests = await _reportRepo.GetSpecialRequestsByIdsAsync(booking.SpecialRequestIds);

                    dayBookings.Add(new
                    {
                        booking.Id,
                        booking.GuestName,
                        booking.RoomId,
                        SpecialRequests = specialRequests.Select(r => r.Description).ToList()
                    });
                }

                calendar[date.ToString("yyyy-MM-dd")] = dayBookings;
            }

            return Ok(calendar);
        }

    }
}
