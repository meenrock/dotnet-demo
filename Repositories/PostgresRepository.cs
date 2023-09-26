using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_demo.Models.User;
using Npgsql;
using System.Data;

namespace dotnet_demo.Repositories
{
    public class PostgresDBContext : DbContext
    {
        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
    public class PostgresRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PostgresRepository(string connectionString, IConfiguration configuration)
        {
            _connectionString = connectionString;
            _configuration = configuration;
        }

        private NpgsqlConnection CreateConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string sql)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }

    }
}
