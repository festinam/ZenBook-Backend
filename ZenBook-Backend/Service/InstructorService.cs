using System.Collections.Generic;
using System.Threading.Tasks;
using ZenBook_Backend.Models;
using ZenBook_Backend.Repositories;

namespace ZenBook_Backend.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IGenericRepository<Instructor> _instructorRepository;

        public InstructorService(IGenericRepository<Instructor> instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            return await _instructorRepository.GetAllAsync();
        }

        public async Task<Instructor> GetInstructorByIdAsync(int id)
        {
            return await _instructorRepository.GetByIdAsync(id);
        }

        public async Task<Instructor> CreateInstructorAsync(Instructor instructor)
        {
            await _instructorRepository.AddAsync(instructor);
            return instructor;
        }

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            await _instructorRepository.UpdateAsync(instructor);
        }

        public async Task DeleteInstructorAsync(int id)
        {
            await _instructorRepository.DeleteAsync(id);
        }
    }
}
