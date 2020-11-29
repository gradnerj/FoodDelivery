﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class GenericRepository<T> : IGenericRepository<T> where T : class {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context) {
            _context = context;
        }
        public virtual T GetById(int id) {
            return _context.Set<T>().Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate = null, bool asNoTracking = false, string includes = null) {
            
            if(includes == null) { 
                if (asNoTracking) {
                    return _context.Set<T>()
                    .AsNoTracking()
                    .Where(predicate)
                    .FirstOrDefault();
                } else {
                    return _context.Set<T>()
                        .Where(predicate)
                        .FirstOrDefault();
                }
            } else {
                IQueryable<T> queryable = _context.Set<T>();
                foreach (var includeProperty in includes.Split(new char[]
                        {','}, StringSplitOptions.RemoveEmptyEntries)) {
                    queryable = queryable.Include(includeProperty);
                }
                if (asNoTracking) {
                    return queryable
                        .AsNoTracking()
                        .Where(predicate)
                        .FirstOrDefault();
                } else {
                    return queryable
                        .Where(predicate)
                        .FirstOrDefault();
                }
            }
        }

        public virtual IEnumerable<T> List() {
            return _context.Set<T>().ToList().AsEnumerable();
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate = null, Expression<Func<T, int>> orderBy = null,string includes = null) {
            IQueryable<T> queryable = _context.Set<T>();
            if (predicate != null && includes == null) {
                return _context.Set<T>()
                       .Where(predicate)
                       .AsEnumerable();
            }
            else if(includes !=null) {
                foreach (var includeProperty in includes.Split(new char[]
                        {','}, StringSplitOptions.RemoveEmptyEntries)) {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if(predicate == null) {
                if (orderBy == null) {
                    return queryable.AsEnumerable();
                } else {
                    return queryable.OrderBy(orderBy).ToList().AsEnumerable();
                }
            } else {
                if(orderBy == null) {
                    return queryable.Where(predicate).ToList().AsEnumerable();
                } else {
                    return queryable.Where(predicate).OrderBy(orderBy).ToList().AsEnumerable();
                }
            }
         }

        public void Update(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity) {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public void Delete(IEnumerable<T> entities) {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }
        public void Add(T entity) {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
    }
}
