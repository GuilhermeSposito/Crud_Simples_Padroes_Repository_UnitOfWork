using ApiCatalogoTeste2.Repositorys.Categorias;
using ApiCatalogoTeste2.Repositorys.Produtos;

namespace ApiCatalogoTeste2.Repositorys.UnitOfWork;

public interface IUnitOfWork
{
    ICategoriasRepository CategoriasRepository { get; }
    IProdutosRepository ProdutosRepository { get; }

    void Commit();
}
