using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FoodDelivery.DataAccess.IRepository {
    // Define generic type as a class
    public interface IRepository<T> where T : class {
        //Get Object Id
        T Get(int id);

        //Get All Objects
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            //Comma Delimited String
            string includeProperties = null
        );

        //Get the First or Default
        T GetFirstorDefault(
            Expression<Func<T, bool>> filter = null,
            //Comma Delimited String
            string includeProperties = null
        );
        //Add 
        void Add(T entity);

        //Remove by Id
        void Remove(int id);

        //Remove by Object
        void Remove(T entity);

        //Remove a List of Objects
        void RemoveRange(IEnumerable<T> entities);
    }
}
