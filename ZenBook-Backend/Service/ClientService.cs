using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ZenBook_Backend.DTOs;
using ZenBook_Backend.Models;
using ZenBook_Backend.Repositories;
using ZenBook_Backend.Shared;

namespace ZenBook_Backend.Services
{
    public class ClientService : IClientService
    {
        private readonly IGenericRepository<Client> _clientRepository;

        public ClientService(IGenericRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            var clientDto = new ClientDto
            {
                Id = client.Id,
                FullName = client.FullName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                DateOfBirth = client.DateOfBirth,
                Address = client.Address,
                ProfilePictureUrl = client.ProfilePictureUrl,
                IsActive = client.IsActive
            };

            return clientDto;

        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            await _clientRepository.AddAsync(client);
            return client;
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }
    }
}
