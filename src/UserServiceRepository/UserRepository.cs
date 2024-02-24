using MongoDB.Driver;
using UserServiceRepository.Interface;
using UserServiceRepository.Model;

namespace UserServiceRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<User>(nameof(User));
        }

        public async Task<User> CreateAsync(User model)
        {
            model.Id = Guid.NewGuid().ToString();
            await _collection.InsertOneAsync(model);
            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync(int offset, int fetch)
        {
            var filter = Builders<User>.Filter.Empty;

            return _collection
                .Find(filter)
                .Skip(offset)
                .Limit(fetch)
                .ToList();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _collection.Find(model => model.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, User model)
        {
            await _collection.FindOneAndUpdateAsync(
                Builders<User>.Filter.Eq(c => c.Id, id),
                Builders<User>.Update
                    .Set(c => c.Name, model.Name)
                    .Set(c => c.Username, model.Username)
                    .Set(c => c.Email, model.Email)
                    .Set(c => c.PhoneNumber, model.PhoneNumber),
                new FindOneAndUpdateOptions<User> { ReturnDocument = ReturnDocument.After });
        }
    }
}
