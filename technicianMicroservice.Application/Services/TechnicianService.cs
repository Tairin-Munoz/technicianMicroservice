using technicianMicroservice.Domain.Entities;
using technicianMicroservice.Domain.Ports;

namespace technicianMicroservice.Application.Services
{
    public class TechnicianService
    {
        private readonly ITechnicianRepository _repository;

        public TechnicianService(ITechnicianRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Technician>> GetAll() => _repository.GetAllAsync();
        public Task<Technician?> GetById(int id) => _repository.GetByIdAsync(id);
        public Task<bool> Create(Technician technician, int userId) => _repository.CreateAsync(technician, userId);
        public Task<bool> Update(Technician technician, int userId) => _repository.UpdateAsync(technician, userId);
        public Task<bool> DeleteById(int id, int userId) => _repository.DeleteByIdAsync(id, userId);
    }
}
