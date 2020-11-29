﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces {
    public interface IGenericRepository<T> where T : class {
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func< T, int>> orderBy = null,  string includes = null);
        void Add(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Update(T entity);
    }
}
