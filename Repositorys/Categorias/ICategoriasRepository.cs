using ApiCatalogoTeste2.Filters.Paginacao;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Repositorys.Generics;

namespace ApiCatalogoTeste2.Repositorys.Categorias;

public interface ICategoriasRepository : IRepository<Categoria>
{
    IEnumerable<Categoria> GetCategoriaPagination(CategoriaParams CategoriaParam);
}
