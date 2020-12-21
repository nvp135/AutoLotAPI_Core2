﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoLotDAL_Core2.EF;
using AutoLotDAL_Core2.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoLotDAL_Core2.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly AutoLotContext _db;
        protected AutoLotContext Context => _db;

        public BaseRepo() : this(new AutoLotContext())
        { }

        public BaseRepo(AutoLotContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        private int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw;
            }
            catch (DbUpdateException ex)
            {

                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Delete(int id, byte[] timestamp)
        {
            _db.Entry(new T() { Id = id, Timestamp = timestamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public List<T> ExecuteQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public List<T> ExecuteQuery(string sql, object[] sqlParameters)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetAll()
        {
            return _table.ToList();
        }

        public List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
        {
            return (ascending ? _table.OrderBy(orderBy) : _table.OrderByDescending(orderBy)).ToList();
        }

        public T GetOne(int? id)
        {
            return _table.Find(id);
        }

        public List<T> GetSome(Expression<Func<T, bool>> where)
        {
            return _table.Where(where).ToList();
        }

        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }

        public int Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return SaveChanges();
        }
    }
}
