using System.Collections.Generic;
using System.Threading.Tasks;
using ZenBook_Backend.DTOs;
using ZenBook_Backend.Models;
using ZenBook_Backend.Shared;

namespace ZenBook_Backend.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
    }
}
