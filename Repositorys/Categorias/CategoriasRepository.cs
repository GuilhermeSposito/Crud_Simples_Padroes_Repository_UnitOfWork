using ApiCatalogoTeste2.Context;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Repositorys.Generics;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ApiCatalogoTeste2.Repositorys.Categorias;

public class CategoriasRepository : Repository<Categoria>, ICategoriasRepository
{
    public CategoriasRepository(AppDbContext context) : base(context)
    {
    }
}
