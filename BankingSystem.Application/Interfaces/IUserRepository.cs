using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid userId);
        Task<User> LoginAsync(string username, string password);
        Task<bool> ChangeRoleAsync(Guid userId, string newRole);
    }
}
