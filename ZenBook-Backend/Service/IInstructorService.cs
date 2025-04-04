using System.Collections.Generic;
using System.Threading.Tasks;
using ZenBook_Backend.Models;

namespace ZenBook_Backend.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(int id);
        Task<Instructor> CreateInstructorAsync(Instructor instructor);
        Task UpdateInstructorAsync(Instructor instructor);
        Task DeleteInstructorAsync(int id);
    }
}
