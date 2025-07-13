using Hotel_Booking_Management.DataManage;
using Hotel_Booking_Management.Dtos;
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
    public class BookingsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(DataStore.Bookings);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound("Booking not found");
            return Ok(booking);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookingDto dto)
        {
            dto.Id = DataStore.Bookings.Count > 0 ? DataStore.Bookings.Max(b => b.Id) + 1 : 1;
            DataStore.Bookings.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BookingDto updated)
        {
            var existing = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
            if (existing == null) return NotFound("Booking not found");

            existing.RoomId = updated.RoomId;
            existing.CheckInDate = updated.CheckInDate;
            existing.CheckOutDate = updated.CheckOutDate;
            existing.SpecialRequestIds = updated.SpecialRequestIds;
            existing.GuestName = updated.GuestName;
            existing.IsRecurring = updated.IsRecurring;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound("Booking not found");

            DataStore.Bookings.Remove(booking);
            return Ok("Booking deleted");
        }
    }
}
