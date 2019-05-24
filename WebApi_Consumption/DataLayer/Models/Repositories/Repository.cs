using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


public class Repository<T> : IRepository<T> where T : class
{
    protected DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public void Add(T record) => _context.Set<T>().Add(record);
    public void AddRange(IEnumerable<T> records) => _context.Set<T>().AddRange(records);
    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);
    public T Get(int id) => _context.Set<T>().Find(id);
    public IQueryable<T> GetAll() => _context.Set<T>();
    public void Remove(T record) => _context.Set<T>().Remove(record);
    public void RemoveRange(IEnumerable<T> records) => _context.Set<T>().RemoveRange(records);
    public int Save() => _context.SaveChanges();
}
