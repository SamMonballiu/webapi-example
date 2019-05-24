using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    T Get(int id);
    IQueryable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    void Add(T record);
    void AddRange(IEnumerable<T> records);

    void Remove(T record);
    void RemoveRange(IEnumerable<T> records);

    int Save();
    
}
