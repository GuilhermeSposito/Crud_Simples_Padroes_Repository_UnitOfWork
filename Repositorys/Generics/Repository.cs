using ApiCatalogoTeste2.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoTeste2.Repositorys.Generics;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }


    public IEnumerable<T> GettAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }


    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Remove(entity);

        return entity;  
    }


    public T Update(T entity)
    {
        _context.Set<T>().Entry(entity).State = EntityState.Modified;

        return entity;
    }
}
