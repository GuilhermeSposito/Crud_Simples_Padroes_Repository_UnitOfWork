using ApiCatalogoTeste2.Context;
using ApiCatalogoTeste2.Filters.Paginacao;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Repositorys.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ApiCatalogoTeste2.Repositorys.Categorias;

public class CategoriasRepository : Repository<Categoria>, ICategoriasRepository
{
    public CategoriasRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Categoria> GetCategoriaPagination(CategoriaParams CategoriaParam)
    {
       return  GettAll()
             .OrderBy(x => x.Nome)
             .Skip((CategoriaParam.PageNumber - 1) * CategoriaParam.PageSize)
             .Take(CategoriaParam.PageSize)
             .ToList();
    }
}
