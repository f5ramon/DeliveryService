using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DeliveryService.Models;

namespace DeliveryService.Data.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : BaseModel
    {
        private DbContext dbContext;
        private DbSet<T> dbSet;
        private bool disposed = false;

        protected virtual string[] NestedProperties { get; set; }

        public GenericRepository(DeliveryServiceDBContext dbcontext)
        {
            this.dbContext = dbcontext;
            this.dbSet = dbContext.Set<T>();
            this.dbContext.Configuration.LazyLoadingEnabled = false;
            this.dbContext.Configuration.AutoDetectChangesEnabled = false;
            this.dbContext.Configuration.ProxyCreationEnabled = false;
        }

        private string[] GetKeyNames()
        {
            var objectSet = ((IObjectContextAdapter)this.dbContext).ObjectContext.CreateObjectSet<T>();
            string[] keyNames = objectSet.EntitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();
            return keyNames;
        }

        private string[] GetProperties()
        {
            var objectSet = ((IObjectContextAdapter)this.dbContext).ObjectContext.CreateObjectSet<T>();
            string[] keyNames = objectSet.EntitySet.ElementType.Properties.Select(k => k.Name).ToArray();
            return keyNames;
        }

        private object[] GetPropertiesValues(T entity)
        {
            var keyNames = GetProperties();
            Type type = typeof(T);

            var keys = new object[keyNames.Length];
            for (int i = 0; i < keyNames.Length; i++)
                keys[i] = type.GetProperty(keyNames[i]).GetValue(entity, null);

            return keys;
        }

        private object[] GetPrimaryKeys(T entity)
        {
            var keyNames = GetKeyNames();
            Type type = typeof(T);

            var keys = new object[keyNames.Length];
            for (int i = 0; i < keyNames.Length; i++)
                keys[i] = type.GetProperty(keyNames[i]).GetValue(entity, null);

            return keys;
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    this.dbContext.Dispose();
            }
            disposed = true;
        }

        public virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<T> query = this.dbSet.AsNoTracking();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty).AsNoTracking();
            if (orderBy != null)
                return orderBy(query).AsNoTracking();
            return query.Where(x => x.Active).AsNoTracking();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            params string[] includeProperties)
        {
            if (this.NestedProperties != null && (includeProperties == null || includeProperties.Length == 0))
            {
                includeProperties = this.NestedProperties;
            }
            return GetQuery(filter, orderby, includeProperties);
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params string[] includeProperties)
        {
            if (this.NestedProperties != null && (includeProperties == null || includeProperties.Length == 0))
            {
                includeProperties = this.NestedProperties;
            }
            return GetQuery(filter, orderby, includeProperties).ToList();
        }

        public virtual T GetById(object id)
        {
            return this.Get(x => x.Id == (int)id).FirstOrDefault();
        }

        public virtual T GetByIdAttched(object id)
        {
            return this.dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            entity.Active = true;
            this.dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            entity.Active = true;
            var entry = this.dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var key = GetPrimaryKeys(entity)[0];
                var currentEntry = GetById(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.dbContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    this.dbSet.Attach(entity);
                    this.dbContext.Entry(entity).State = EntityState.Modified;
                }
            }
            else if (entry.State == EntityState.Unchanged)
                this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateAll(T entity)
        {
            var prop = entity.GetType().GetProperties().Where(p => !p.PropertyType.FullName.Contains("System") || p.PropertyType.Name.Contains("ICollection")).ToList();

            foreach (var item in prop)
            {
                var v = item.GetMethod;
            }

            var entry = this.dbContext.Entry(entity);
            var keys = GetPropertiesValues(entity);
            if (entry.State == EntityState.Detached)
            {



                var key = GetPrimaryKeys(entity)[0];
                var currentEntry = GetById(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.dbContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    this.dbSet.Attach(entity);
                    this.dbContext.Entry(entity).State = EntityState.Modified;
                }
            }
            else if (entry.State == EntityState.Unchanged)
                this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void HardDelete(T entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
                this.dbSet.Attach(entity);
            this.dbSet.Remove(entity);
        }

        public virtual void Delete(T entity)
        {
            entity.Active = false;
            //if (this.dbContext.Entry(entity).State == EntityState.Detached)
            //    this.dbSet.Attach(entity);
            //this.dbSet.Remove(entity);
            var entry = this.dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var key = GetPrimaryKeys(entity)[0];
                var currentEntry = GetById(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.dbContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    this.dbSet.Attach(entity);
                    this.dbContext.Entry(entity).State = EntityState.Modified;
                }
            }
            else if (entry.State == EntityState.Unchanged)
                this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            Delete(GetByIdAttched(id));
        }

        public virtual void HardDelete(object id)
        {
            HardDelete(GetById(id));
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return GetQuery(filter).Any();
        }

        public virtual void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual int ExecuteStoredProcedure(string procedureName, params KeyValuePair<string, object>[] parameters)
        {
            string cmdText = GetCommandText(procedureName, CommandType.StoredProcedure, parameters);
            dbContext.Database.CommandTimeout = 120;
            int identity = 0;

            if (parameters != null && parameters.Length > 0)
            {
                identity = dbContext.Database.ExecuteSqlCommand(cmdText, GetNamedParameters(parameters).ToArray());
            }
            else
            {
                identity = dbContext.Database.ExecuteSqlCommand(cmdText);
            }

            return identity;
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = null;

            if (includes.Length > 0)
            {
                query = this.dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? this.dbSet : (IQueryable<T>)query;
        }

        protected string GetCommandText(string commandText, System.Data.CommandType type, params KeyValuePair<string, object>[] parameters)
        {
            var text = new StringBuilder(string.Format("{0}{1}", type == System.Data.CommandType.StoredProcedure ? "EXEC " : string.Empty, commandText));
            if (type == System.Data.CommandType.StoredProcedure && parameters != null && parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    text.AppendFormat(" @{0},", parameter.Key);
                }
                return text.Remove(text.Length - 1, 1).ToString();
            }
            return text.ToString();
        }


        protected DbParameter GetNamedParameter(KeyValuePair<string, object> parameter)
        {
            var namedParameter = new SqlParameter { ParameterName = parameter.Key, Value = parameter.Value };
            return namedParameter;
        }

        protected List<DbParameter> GetNamedParameters(KeyValuePair<string, object>[] parameters)
        {
            List<DbParameter> namedParameters = new List<DbParameter>();
            foreach (var kvp in parameters)
            {
                namedParameters.Add(GetNamedParameter(kvp));
            }
            return namedParameters;
        }

    }
}