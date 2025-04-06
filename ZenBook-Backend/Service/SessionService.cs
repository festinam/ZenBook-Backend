using System.Collections.Generic;
using System.Threading.Tasks;
using ZenBook_Backend.Models;
using ZenBook_Backend.Repositories;

namespace ZenBook_Backend.Services
{
    public class SessionService : ISessionService
    {
        private readonly IGenericRepository<Session> _sessionRepository;

        public SessionService(IGenericRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IEnumerable<Session>> GetAllSessionsAsync()
        {
            return await _sessionRepository.GetAllAsync();
        }

        public async Task<Session> GetSessionByIdAsync(int id)
        {
            return await _sessionRepository.GetByIdAsync(id);
        }

        public async Task<Session> CreateSessionAsync(Session session)
        {
            await _sessionRepository.AddAsync(session);
            return session;
        }

        public async Task UpdateSessionAsync(Session session)
        {
            await _sessionRepository.UpdateAsync(session);
        }

        public async Task DeleteSessionAsync(int id)
        {
            await _sessionRepository.DeleteAsync(id);
        }
    }
}
