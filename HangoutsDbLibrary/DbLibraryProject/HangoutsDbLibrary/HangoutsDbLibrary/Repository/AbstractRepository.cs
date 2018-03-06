using HangoutsDbLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace HangoutsDbLibrary.Repository
{
    public class AbstractRepository<E> : InterfaceRepository<E> where E : class
    {
        internal HangoutsContext context;
        internal DbSet<E> dbSet;

        public AbstractRepository(HangoutsContext context){
            this.context = context;
            dbSet = context.Set<E>();
        }

        public void Add(E e)
        {
            dbSet.Add(e);
        }

        public void Delete(E e)
        {
            dbSet.Remove(e);
        }

        public E FindBy(Expression<Func<E, bool>> predicate)
        {
            return dbSet.Where(predicate).FirstOrDefault();
        }

        public IQueryable<E> GetAll()
        {
            return dbSet;
        }

        public IQueryable<E> GetAllBy(Expression<Func<E, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public void Update(E e)
        {
            dbSet.Attach(e);
            context.Entry(e).State = EntityState.Modified;
        }
    }
}
