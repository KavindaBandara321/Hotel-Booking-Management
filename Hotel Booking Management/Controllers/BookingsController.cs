using Hotel_Booking_Management.DataManage;
using Hotel_Booking_Management.Dtos;
using Hotel_Booking_Management.IRepository;
using Hotel_Booking_Management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class BookingsController : ControllerBase
    //{
    //    [HttpGet]
    //    public IActionResult GetAll() => Ok(DataStore.Bookings);

    //    [HttpGet("{id}")]
    //    public IActionResult Get(int id)
    //    {
    //        var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
    //        if (booking == null) return NotFound("Booking not found");
    //        return Ok(booking);
    //    }

    //    [HttpPost]
    //    public IActionResult Create([FromBody] BookingDto dto)
    //    {
    //        try
    //        {
    //            if (!ModelState.IsValid)
    //            {
    //                return BadRequest(ModelState);
    //            }

    //            dto.Id = DataStore.Bookings.Count > 0 ? DataStore.Bookings.Max(b => b.Id) + 1 : 1;
    //            DataStore.Bookings.Add(dto);
    //            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(StatusCodes.Status500InternalServerError, new
    //            {
    //                Message = "An unexpected error occurred.",
    //                Details = ex.Message
    //            });
    //        }
    //    }

    //    [HttpPut("{id}")]
    //    public IActionResult Update(int id, [FromBody] BookingDto updated)
    //    {
    //        var existing = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
    //        if (existing == null) return NotFound("Booking not found");

    //        existing.RoomId = updated.RoomId;
    //        existing.CheckInDate = updated.CheckInDate;
    //        existing.CheckOutDate = updated.CheckOutDate;
    //        existing.SpecialRequestIds = updated.SpecialRequestIds;
    //        existing.GuestName = updated.GuestName;
    //        existing.IsRecurring = updated.IsRecurring;

    //        return Ok(existing);
    //    }

    //    [HttpDelete("{id}")]
    //    public IActionResult Delete(int id)
    //    {
    //        var booking = DataStore.Bookings.FirstOrDefault(b => b.Id == id);
    //        if (booking == null) return NotFound("Booking not found");

    //        DataStore.Bookings.Remove(booking);
    //        return Ok("Booking deleted");
    //    }
    //}

    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingsController(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return NotFound("Booking not found");
            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBooking = await _bookingRepo.AddAsync(booking);
            return CreatedAtAction(nameof(Get), new { id = createdBooking.Id }, createdBooking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Booking booking)
        {
            if (id != booking.Id)
                return BadRequest("Id mismatch");

            var existing = await _bookingRepo.GetByIdAsync(id);
            if (existing == null) return NotFound("Booking not found");

            // Optionally update fields one by one or just update entire entity
            existing.RoomId = booking.RoomId;
            existing.CheckInDate = booking.CheckInDate;
            existing.CheckOutDate = booking.CheckOutDate;
            existing.SpecialRequestIds = booking.SpecialRequestIds;
            existing.GuestName = booking.GuestName;
            existing.IsRecurring = booking.IsRecurring;

            var updatedBooking = await _bookingRepo.UpdateAsync(existing);
            return Ok(updatedBooking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookingRepo.DeleteAsync(id);
            if (!deleted) return NotFound("Booking not found");
            return Ok("Booking deleted");
        }
    }
}
