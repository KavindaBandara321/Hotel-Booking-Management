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
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(DataStore.Rooms);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound("Room not found");
            return Ok(room);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RoomDto dto)
        {
            dto.Id = DataStore.Rooms.Count > 0 ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
            DataStore.Rooms.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RoomDto updated)
        {
            var existing = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (existing == null) return NotFound("Room not found");

            existing.RoomType = updated.RoomType;
            existing.Capacity = updated.Capacity;
            existing.PricePerNight = updated.PricePerNight;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound("Room not found");

            DataStore.Rooms.Remove(room);
            return Ok("Room deleted");
        }
    }
}
