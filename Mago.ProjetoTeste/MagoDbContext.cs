using Mago.ProjetoTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace Mago.ProjetoTeste
{
    public class MagoDbContext :DbContext
    {
        public MagoDbContext(DbContextOptions<MagoDbContext> options)
            : base(options)
        {
            
        }      

        public DbSet<Cliente> LstCliente { get; set; }
    }
}
