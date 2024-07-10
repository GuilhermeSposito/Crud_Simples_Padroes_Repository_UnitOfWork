using System.Linq.Expressions;

namespace ApiCatalogoTeste2.Repositorys.Generics;

public interface IRepository<T>
{
    IEnumerable<T> GettAll();
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
