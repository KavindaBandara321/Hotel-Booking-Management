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
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult GetAll() => Ok(DataStore.Rooms);

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        //    if (room == null) return NotFound("Room not found");
        //    return Ok(room);
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody] RoomDto dto)
        //{
        //    dto.Id = DataStore.Rooms.Count > 0 ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
        //    DataStore.Rooms.Add(dto);
        //    Console.WriteLine(dto);
        //    return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] RoomDto updated)
        //{
        //    var existing = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        //    if (existing == null) return NotFound("Room not found");

        //    existing.RoomType = updated.RoomType;
        //    existing.Capacity = updated.Capacity;
        //    existing.PricePerNight = updated.PricePerNight;

        //    return Ok(existing);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        //    if (room == null) return NotFound("Room not found");

        //    DataStore.Rooms.Remove(room);
        //    return Ok("Room deleted");
        //}
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return NotFound("Room not found");
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Room room)
        {
            var createdRoom = await _roomRepository.AddAsync(room);
            return CreatedAtAction(nameof(Get), new { id = createdRoom.Id }, createdRoom);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Room room)
        {
            if (id != room.Id) return BadRequest("ID mismatch");

            var existingRoom = await _roomRepository.GetByIdAsync(id);
            if (existingRoom == null) return NotFound("Room not found");

            existingRoom.RoomType = room.RoomType;
            existingRoom.Capacity = room.Capacity;
            existingRoom.PricePerNight = room.PricePerNight;

            var updatedRoom = await _roomRepository.UpdateAsync(existingRoom);
            return Ok(updatedRoom);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _roomRepository.DeleteAsync(id);
            if (!deleted) return NotFound("Room not found");
            return Ok("Room deleted");
        }
    }
}
