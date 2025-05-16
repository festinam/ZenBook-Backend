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
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients([FromHeader(Name = "X-Tenant-ID")] string tenantId)
        {
            var clients = await _clientService.GetAllClientsAsync();
            var clientDtos = clients.Select(c => new ClientDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                DateOfBirth = c.DateOfBirth,
                Address = c.Address,
                ProfilePictureUrl = c.ProfilePictureUrl,
                IsActive = c.IsActive
            });
            return Ok(clientDtos);
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]

        public async Task<ActionResult<ClientDto>> GetClient(int id, [FromHeader(Name = "X-Tenant-ID")] string tenantId
)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (!client.IsSuccess)
            {
                if(client.StatusCode is StatusCodes.Status404NotFound)
                    return NotFound(client);

                return BadRequest(client);
            }

            return Ok(client);
        }

        // POST: api/clients
        [HttpPost]
        public async Task<ActionResult<ClientDto>> CreateClient(ClientDto clientDto)
        {
            // Map DTO to domain model
            var client = new Client
            {
                FullName = clientDto.FullName,
                Email = clientDto.Email,
                PhoneNumber = clientDto.PhoneNumber,
                DateOfBirth = clientDto.DateOfBirth,
                Address = clientDto.Address,
                ProfilePictureUrl = clientDto.ProfilePictureUrl,
                IsActive = clientDto.IsActive
            };

            await _clientService.CreateClientAsync(client);
            clientDto.Id = client.Id; // Set generated Id
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, clientDto);
        }

        // PUT: api/clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientDto clientDto)
        {
            if (id != clientDto.Id)
                return BadRequest();

            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound();

            //// Update properties
            //client.FullName = clientDto.FullName;
            //client.Email = clientDto.Email;
            //client.PhoneNumber = clientDto.PhoneNumber;
            //client.DateOfBirth = clientDto.DateOfBirth;
            //client.Address = clientDto.Address;
            //client.ProfilePictureUrl = clientDto.ProfilePictureUrl;
            //client.IsActive = clientDto.IsActive;

            //await _clientService.UpdateClientAsync(client);
            return NoContent();
        }

        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound();

            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
    }
}
