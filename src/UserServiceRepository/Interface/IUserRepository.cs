using UserServiceRepository.Model;

namespace UserServiceRepository.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User model);
        Task UpdateAsync(string id, User model);
        Task DeleteAsync(string id);
        Task<User> GetByIdAsync(string id);
        Task<IEnumerable<User>> GetAllAsync(int offset, int fetch);
    }
}