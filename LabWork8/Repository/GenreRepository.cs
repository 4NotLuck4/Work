using Dapper;
using LabWork8.Model;
using LabWork8.Repository;

namespace LabWork8
{
    public class GenreRepository(DatabaseContext dbContext) : IRepository<Genre>
    {
        private readonly DatabaseContext _dbContext = dbContext;

        //public Task<int> AddAsync(Genre entity) 
        //    => throw new NotImplementedException();
        //public Task DeleteAsync(int id) 
        //    => throw new NotImplementedException();
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
        //3.4.1
        public async Task<int> AddAsync(Genre entity)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(
                @"INSERT INTO Genre (Name) 
                  OUTPUT INSERTED.Id 
                  VALUES (@Name)",
                entity);
        }
        //3.5.1
        public async Task DeleteAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "DELETE FROM Genre WHERE Id = @Id",
                new { Id = id });
        }
    }
}
