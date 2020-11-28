using System;
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

        public virtual IEnumerable<T> List() {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate) {
            return _context.Set<T>()
                   .Where(predicate)
                   .AsEnumerable();
        }

        public void Update(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity) {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Add(T entity) {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
    }
}
