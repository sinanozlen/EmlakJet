using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly EmlakJetContext _emlakJetContext;

        public GenericRepository(EmlakJetContext emlakJetContext)
        {
            _emlakJetContext = emlakJetContext;
        }

        public void Add(T entity)
        {
            _emlakJetContext.Add(entity);
            _emlakJetContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _emlakJetContext.Remove(entity);
            _emlakJetContext.SaveChanges();
        }


        public T GetbyID(int ID)
        {
            var values= _emlakJetContext.Set<T>().Find(ID);
            return values;
        }

        public List<T> GetListAll()
        {
           var values= _emlakJetContext.Set<T>().ToList();
            return values;
        }

        public void Update(T entity)
        {
            _emlakJetContext.Update(entity);
            _emlakJetContext.SaveChanges();
        }
    }
}
