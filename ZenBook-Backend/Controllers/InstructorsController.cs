using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenBook_Backend.DTOs;
using ZenBook_Backend.Models;
using ZenBook_Backend.Services;

namespace ZenBook_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        // GET: api/instructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructors()
        {
            var instructors = await _instructorService.GetAllInstructorsAsync();
            var instructorDtos = instructors.Select(i => new InstructorDto
            {
                Id = i.Id,
                FullName = i.FullName,
                Email = i.Email,
                PhoneNumber = i.PhoneNumber,
                DateOfBirth = i.DateOfBirth,
                Department = i.Department,
                Bio = i.Bio,
                IsActive = i.IsActive
            });
            return Ok(instructorDtos);
        }

        // GET: api/instructors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
                return NotFound();

            var instructorDto = new InstructorDto
            {
                Id = instructor.Id,
                FullName = instructor.FullName,
                Email = instructor.Email,
                PhoneNumber = instructor.PhoneNumber,
                DateOfBirth = instructor.DateOfBirth,
                Department = instructor.Department,
                Bio = instructor.Bio,
                IsActive = instructor.IsActive
            };
            return Ok(instructorDto);
        }

        // POST: api/instructors
        [HttpPost]
        public async Task<ActionResult<InstructorDto>> CreateInstructor(InstructorDto instructorDto)
        {
            // Map DTO to domain model
            var instructor = new Instructor
            {
                FullName = instructorDto.FullName,
                Email = instructorDto.Email,
                PhoneNumber = instructorDto.PhoneNumber,
                DateOfBirth = instructorDto.DateOfBirth,
                Department = instructorDto.Department,
                Bio = instructorDto.Bio,
                IsActive = instructorDto.IsActive
            };

            await _instructorService.CreateInstructorAsync(instructor);
            instructorDto.Id = instructor.Id; // Set the generated Id

            return CreatedAtAction(nameof(GetInstructor), new { id = instructor.Id }, instructorDto);
        }

        // PUT: api/instructors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstructor(int id, InstructorDto instructorDto)
        {
            if (id != instructorDto.Id)
                return BadRequest();

            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
                return NotFound();

            // Map the changes from DTO to the domain model
            instructor.FullName = instructorDto.FullName;
            instructor.Email = instructorDto.Email;
            instructor.PhoneNumber = instructorDto.PhoneNumber;
            instructor.DateOfBirth = instructorDto.DateOfBirth;
            instructor.Department = instructorDto.Department;
            instructor.Bio = instructorDto.Bio;
            instructor.IsActive = instructorDto.IsActive;

            await _instructorService.UpdateInstructorAsync(instructor);
            return NoContent();
        }

        // DELETE: api/instructors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
                return NotFound();

            await _instructorService.DeleteInstructorAsync(id);
            return NoContent();
        }
    }
}
