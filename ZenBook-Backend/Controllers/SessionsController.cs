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
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // GET: api/sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            var sessionDtos = sessions.Select(s => new SessionDto
            {
                Id = s.Id,
                CourseId = s.CourseId,
                InstructorId = s.InstructorId,
                ClientId = s.ClientId,
                SessionDate = s.SessionDate,
                SessionTime = s.SessionTime,
                Location = s.Location,
                Topic = s.Topic,
                IsCompleted = s.IsCompleted
            });
            return Ok(sessionDtos);
        }

        // GET: api/sessions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
                return NotFound();

            var sessionDto = new SessionDto
            {
                Id = session.Id,
                CourseId = session.CourseId,
                InstructorId = session.InstructorId,
                ClientId = session.ClientId,
                SessionDate = session.SessionDate,
                SessionTime = session.SessionTime,
                Location = session.Location,
                Topic = session.Topic,
                IsCompleted = session.IsCompleted
            };

            return Ok(sessionDto);
        }

        // POST: api/sessions
        [HttpPost]
        public async Task<ActionResult<SessionDto>> CreateSession(SessionDto sessionDto, [FromHeader(Name = "X-Tenant-ID")] string tenantId)
        {
            // Map the DTO to the domain model.
            var session = new Session
            {
                CourseId = sessionDto.CourseId,
                InstructorId = sessionDto.InstructorId,
                ClientId = sessionDto.ClientId,
                SessionDate = sessionDto.SessionDate,
                SessionTime = sessionDto.SessionTime,
                Location = sessionDto.Location,
                TenantId = tenantId,
                Topic = sessionDto.Topic,
                IsCompleted = sessionDto.IsCompleted
            };

            await _sessionService.CreateSessionAsync(session);
            sessionDto.Id = session.Id;  // Set the generated ID

            return CreatedAtAction(nameof(GetSession), new { id = session.Id }, sessionDto);
        }

        // PUT: api/sessions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, SessionDto sessionDto)
        {
            if (id != sessionDto.Id)
                return BadRequest();

            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
                return NotFound();

            // Update the domain model properties from the DTO.
            session.CourseId = sessionDto.CourseId;
            session.InstructorId = sessionDto.InstructorId;
            session.ClientId = sessionDto.ClientId;
            session.SessionDate = sessionDto.SessionDate;
            session.SessionTime = sessionDto.SessionTime;
            session.Location = sessionDto.Location;
            session.Topic = sessionDto.Topic;
            session.IsCompleted = sessionDto.IsCompleted;

            await _sessionService.UpdateSessionAsync(session);
            return NoContent();
        }

        // DELETE: api/sessions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
                return NotFound();

            await _sessionService.DeleteSessionAsync(id);
            return NoContent();
        }
    }
}
