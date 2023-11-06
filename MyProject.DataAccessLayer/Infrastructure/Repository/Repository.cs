﻿using Microsoft.EntityFrameworkCore;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
using MyProject.DataAccessLayer.Data;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyProject.Models;
using MyProject.Models.ViewModels;

namespace MyProject.DataAccessLayer.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            // _context.Products.Include(x => x.Category);
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public IEnumerable<T> GetAlls(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.ToList();
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }




        //public T GetById(int id, params Expression<Func<T, object>>[] includeProperties)
        //{
        //    IQueryable<T> query = _dbSet;
        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);  
        //    }
        //    return query.FirstOrDefault(x => x.Id == id);
        //}

        public T GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(predicate);
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
        }

        
    }

}