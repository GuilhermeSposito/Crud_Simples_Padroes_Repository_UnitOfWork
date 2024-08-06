using ApiCatalogoTeste2.Context;
using ApiCatalogoTeste2.Filters.Paginacao;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Repositorys.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace ApiCatalogoTeste2.Repositorys.Produtos;

public class ProdutosRepository : Repository<Produto>, IProdutosRepository
{
    public ProdutosRepository(AppDbContext context) : base(context)
    {

    }

    public IEnumerable<Produto> GetProdutosPaginado(ProdutosParameters produtoParams)
    {
        return GettAll()
            .OrderBy(p => p.Nome)
            .Skip((produtoParams.PageNumber - 1) * produtoParams.PageSize)
            .Take(produtoParams.PageSize).ToList();
    }

    public IEnumerable<Produto> GetProdutoPorCategoria(int categoriaId)
    {
        return _context.produtos.Where(x => x.CategoriaId == categoriaId).ToList();
    }
}
