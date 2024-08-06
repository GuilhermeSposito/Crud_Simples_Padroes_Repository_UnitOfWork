using ApiCatalogoTeste2.Filters.Paginacao;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Repositorys.Generics;

namespace ApiCatalogoTeste2.Repositorys.Produtos;

public interface IProdutosRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutoPorCategoria(int categoriaId);
    IEnumerable<Produto> GetProdutosPaginado(ProdutosParameters produtoParams);
}
