using System;

namespace authapi.data.Abstract
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        int Update(T entity);
        int Delete(int id);
    }
}
