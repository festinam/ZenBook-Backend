using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            // Map each domain Course to a CourseDto
            var courseDtos = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Category = c.Category,
                DurationInHours = c.DurationInHours,
                InstructorId = c.InstructorId
            });
            return Ok(courseDtos);
        }

        // GET: api/courses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            var courseDto = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Category = course.Category,
                DurationInHours = course.DurationInHours,
                InstructorId = course.InstructorId
            };
            return Ok(courseDto);
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto courseDto, [FromHeader(Name = "X-Tenant-ID")] string tenantId)
        {
            // Map DTO to domain model
            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                StartDate = courseDto.StartDate,
                EndDate = courseDto.EndDate,
                Category = courseDto.Category,
                DurationInHours = courseDto.DurationInHours,
                TenantId = tenantId,
                InstructorId = courseDto.InstructorId // Make sure the Instructor exists
            };

            await _courseService.CreateCourseAsync(course);
            // Update the DTO with the generated ID
            courseDto.Id = course.Id;
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, courseDto);
        }

        // PUT: api/courses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto courseDto)
        {
            if (id != courseDto.Id)
                return BadRequest();

            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            // Map the updated properties
            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.StartDate = courseDto.StartDate;
            course.EndDate = courseDto.EndDate;
            course.Category = courseDto.Category;
            course.DurationInHours = courseDto.DurationInHours;
            course.InstructorId = courseDto.InstructorId;

            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        // DELETE: api/courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
