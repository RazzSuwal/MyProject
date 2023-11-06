using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.Infrastructure.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAlls(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);
        T GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null);

        //T GetById(int id, params Expression<Func<T, object>>[] includeProperties);
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entity);
    }
}
