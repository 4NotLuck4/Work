﻿using Dapper;
using LabWork8.Model;
using LabWork8.Repository;

namespace LabWork8
{
    public class VisitorRepository(DatabaseContext dbContext) : IRepository<Visitor>
    {
        private readonly DatabaseContext _dbContext = dbContext;

        //public Task<int> AddAsync(Visitor entity)
        //    => throw new NotImplementedException();
        //public Task DeleteAsync(int id)
        //    => throw new NotImplementedException();
        public Task UpdateAsync(Visitor entity)

            => throw new NotImplementedException();
        //3.3.1
        public async Task<Visitor?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Visitor>(
                "SELECT * FROM Visitor WHERE Id = @Id",
                new { Id = id });
        }

        //3.3.2
        public async Task<IEnumerable<Visitor>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Visitor>("SELECT * FROM Visitor");
        }
        //3.4.1
        public async Task<int> AddAsync(Visitor entity)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(
                @"INSERT INTO Visitor (Name, Email, Phone, RegistrationDate) 
                  OUTPUT INSERTED.Id 
                  VALUES (@Name, @Email, @Phone, @RegistrationDate)",
                entity);
        }
        //3.5.1
        public async Task DeleteAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "DELETE FROM Visitor WHERE Id = @Id",
                new { Id = id });
        }
    }
}
