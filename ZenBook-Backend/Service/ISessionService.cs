using System.Collections.Generic;
using System.Threading.Tasks;
using ZenBook_Backend.Models;

namespace ZenBook_Backend.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllSessionsAsync();
        Task<Session> GetSessionByIdAsync(int id);
        Task<Session> CreateSessionAsync(Session session);
        Task UpdateSessionAsync(Session session);
        Task DeleteSessionAsync(int id);
    }
}
