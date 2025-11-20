using technicianMicroservice.Domain.Entities;
namespace technicianMicroservice.Domain.Ports;

public interface ITechnicianRepository
{
    Task<IEnumerable<Technician>> GetAllAsync();
    Task<Technician?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Technician technician, int userId);
    Task<bool> UpdateAsync(Technician technician, int userId);
    Task<bool> DeleteByIdAsync(int id, int userId);
}


