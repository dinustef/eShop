using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name; //vadi ime klase
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T t_toUpdate = items.Find(i => i.Id == t.Id);

            if (t_toUpdate != null)
            {
                t_toUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found.");
            }
        }

        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)      
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found.");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T t_toDelete = items.Find(i => i.Id == Id);

            if (t_toDelete != null)
            {
                items.Remove(t_toDelete);
            }
            else
            {
                throw new Exception(className + " not found.");
            }
        }

    }
}
