using Microsoft.EntityFrameworkCore;
using SO.Domain;

namespace SO.Data
{
    public class ContextoPrincipal : DbContext
    {
        public ContextoPrincipal(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
    }


}
