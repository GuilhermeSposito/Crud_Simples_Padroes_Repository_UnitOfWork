using ApiCatalogoTeste2.Context;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Repositorys.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace ApiCatalogoTeste2.Repositorys.Produtos;

public class ProdutosRepository : Repository<Produto> , IProdutosRepository
{
    public ProdutosRepository(AppDbContext context) : base(context) 
    {
       
    }

    public IEnumerable<Produto> GetProdutoPorCategoria(int categoriaId)
    {
        return _context.produtos.Where(x => x.CategoriaId == categoriaId).ToList();
    }
}
