using ApiCatalogoTeste2.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoTeste2.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
            
    }

    public DbSet<Categoria> categorias { get; set; }    
    public DbSet<Produto> produtos { get; set; }
}
