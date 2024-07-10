using ApiCatalogoTeste2.Context;
using ApiCatalogoTeste2.Repositorys.Categorias;
using ApiCatalogoTeste2.Repositorys.Produtos;

namespace ApiCatalogoTeste2.Repositorys.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private ICategoriasRepository? _CategoriasRepo;

    private IProdutosRepository? _produtosRepo;
    public readonly AppDbContext _Context;
    public UnitOfWork(AppDbContext context)
    {
        _Context = context;
    }

    public IProdutosRepository ProdutosRepository
    {
        get
        {
            if(_produtosRepo == null)
            {
                _produtosRepo = new  ProdutosRepository(_Context);
            }

            return _produtosRepo;
        }
    }

    public ICategoriasRepository CategoriasRepository
    {
        get
        {
            if(_CategoriasRepo == null)
            {
                _CategoriasRepo = new CategoriasRepository(_Context);
            }
            return _CategoriasRepo;
        }
    }


    public void Commit()
    {
        _Context.SaveChanges();
    }

    public void Dispose()
    {
        _Context.Dispose(); 
    }
}
