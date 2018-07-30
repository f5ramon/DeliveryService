using DeliveryService.Data;
using DeliveryService.Data.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using DeliveryService.Models;

namespace DeliveryService.Business
{
    public abstract class BaseBusiness<T, U> where U : BaseModel where T : GenericRepository<U>
    {
        public UnitOfWork UnitOfWork { get; set; }

        public T Repository { get; set; }

        public BaseBusiness(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = UnitOfWork.GetRepository<T>();
        }

        public void Create(U entity)
        {
            Repository.Insert(entity);
            SaveChanges();
        }

        public List<U> GetList(Expression<Func<U, bool>> filter = null,
            Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null)
        {
            return Repository.Get(filter, orderBy).ToList();
        }

        public U GetById(int id)
        {
            return Repository.GetById(id);
        }

        public U GetByIdAttached(int id)
        {
            return Repository.GetByIdAttched(id);
        }

        public void Delete(U entity)
        {
            Repository.Delete(entity);
            SaveChanges();
        }

        public void DeleteById(int id)
        {
            Repository.Delete(id);
            SaveChanges();
        }

        public void HardDeleteById(int id)
        {
            Repository.HardDelete(id);
            SaveChanges();
        }

        public void Update(U entity)
        {
            Repository.Update(entity);
            SaveChanges();
        }
        public void UpdateAll(U entity)
        {
            Repository.UpdateAll(entity);
            SaveChanges();
        }
        public List<U> GetAll()
        {
            return Repository.Get().ToList();
        }
        public List<U> GetAllActive()
        {
            return Repository.Get(x => x.Active).ToList();
        }
        public void SaveChanges()
        {
            UnitOfWork.SaveChanges();
        }

    }
}
