using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_demo.Models.User;

namespace dotnet_demo.Repositories
{
    public class PostgresDBContext : DbContext
    {
        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
