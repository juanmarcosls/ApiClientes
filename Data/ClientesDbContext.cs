using Microsoft.EntityFrameworkCore;
using ClientesAPI.Models;

namespace ClientesAPI.Data
{
    public class ClientesDbContext : DbContext
    {
        public ClientesDbContext(DbContextOptions<ClientesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
