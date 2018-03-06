using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HangoutsDbLibrary.Repository
{
    public interface InterfaceRepository<E> where E : class{
        void Delete(E e);
        void Update(E e);
        void Add(E e);
        IQueryable<E> GetAll();
        IQueryable<E> GetAllBy(Expression<Func<E, bool>> predicate);
        E FindBy(Expression<Func<E, bool>> predicate);



    }
}
