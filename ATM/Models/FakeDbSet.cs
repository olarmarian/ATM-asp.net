using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ATM.Models
{
    public class FakeDbSet<T>:IDbSet<T> where T : class
    {
        private readonly List<T> list = new List<T>();

        public FakeDbSet()
        {
            list = new List<T>();
        }
        public FakeDbSet(IEnumerable<T> contents)
        {
            list = contents.ToList();
        }
        public T Add(T entity)
        {
            list.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            list.Add(entity);
            return entity;
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T Remove(T entity)
        {
            list.Remove(entity);
            return entity;
        }

        TDerivedEntity IDbSet<T>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public ObservableCollection<T> Local => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public Type ElementType => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();


    }
}