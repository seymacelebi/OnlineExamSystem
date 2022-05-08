using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {

        T Get(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter=null );
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        //void Add(T entity);
        //void Delete(T entity);
        //void Update(T entity);
        //List<T> GetAll();
        //List<T> GetListAll(Expression<Func<T, bool>> filter);  //filter 
        //T GetById(int id);

    }
}
