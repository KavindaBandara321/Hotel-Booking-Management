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
    public class SpecialRequestsController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult GetAll() => Ok(DataStore.SpecialRequests);

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var request = DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id);
        //    if (request == null) return NotFound("Special request not found");
        //    return Ok(request);
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody] SpecialRequestDto dto)
        //{
        //    dto.Id = DataStore.SpecialRequests.Count > 0 ? DataStore.SpecialRequests.Max(r => r.Id) + 1 : 1;
        //    DataStore.SpecialRequests.Add(dto);
        //    return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] SpecialRequestDto updated)
        //{
        //    var existing = DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id);
        //    if (existing == null) return NotFound("Special request not found");

        //    existing.Description = updated.Description;

        //    return Ok(existing);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var request = DataStore.SpecialRequests.FirstOrDefault(r => r.Id == id);
        //    if (request == null) return NotFound("Special request not found");

        //    DataStore.SpecialRequests.Remove(request);
        //    return Ok("Special request deleted");
        //}
        private readonly ISpecialRequestRepository _specialRequestRepository;

        public SpecialRequestsController(ISpecialRequestRepository specialRequestRepository)
        {
            _specialRequestRepository = specialRequestRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _specialRequestRepository.GetAllAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = await _specialRequestRepository.GetByIdAsync(id);
            if (request == null) return NotFound("Special request not found");
            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecialRequest specialRequest)
        {
            var created = await _specialRequestRepository.AddAsync(specialRequest);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecialRequest updated)
        {
            if (id != updated.Id)
                return BadRequest("ID mismatch");

            var existing = await _specialRequestRepository.GetByIdAsync(id);
            if (existing == null) return NotFound("Special request not found");

            existing.Description = updated.Description;

            var updatedEntity = await _specialRequestRepository.UpdateAsync(existing);
            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _specialRequestRepository.DeleteAsync(id);
            if (!deleted) return NotFound("Special request not found");
            return Ok("Special request deleted");
        }
    }
}
