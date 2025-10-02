using Dapper;
using LabWork8.Model;
using LabWork8.Repository;

namespace LabWork8
{
    public class GenreRepository(DatabaseContext dbContext) : IRepository<Genre>
    {
        private readonly DatabaseContext _dbContext = dbContext;

        public Task<int> AddAsync(Genre entity) 
            => throw new NotImplementedException();
        public Task DeleteAsync(int id) 
            => throw new NotImplementedException();
        public Task UpdateAsync(Genre entity) 
            => throw new NotImplementedException();
        //3.2.1
        public async Task<Genre?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Genre>(
                "SELECT * FROM Genre WHERE Id = @Id",
                new { Id = id });
        }

        //3.2.2
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Genre>("SELECT * FROM Genre");
        }
    }
}
