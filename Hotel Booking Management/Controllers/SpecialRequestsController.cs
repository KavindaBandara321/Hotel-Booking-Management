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
    public class SpecialRequestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(DataStore.SpecialRequests);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var request = DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id);
            if (request == null) return NotFound("Special request not found");
            return Ok(request);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SpecialRequestDto dto)
        {
            dto.Id = DataStore.SpecialRequests.Count > 0 ? DataStore.SpecialRequests.Max(r => r.Id) + 1 : 1;
            DataStore.SpecialRequests.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SpecialRequestDto updated)
        {
            var existing = DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id);
            if (existing == null) return NotFound("Special request not found");

            existing.Description = updated.Description;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var request = DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id);
            if (request == null) return NotFound("Special request not found");

            DataStore.SpecialRequests.Remove(request);
            return Ok("Special request deleted");
        }
    }
}
