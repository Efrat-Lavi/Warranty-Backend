using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Interfaces;

namespace Warranty.Data
{
    public class Repository<T>:IRepository<T> where T:class
    {
        protected readonly DbSet<T> _dbSet;
        public Repository(DataContext context)
        {
            _dbSet = context.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T> Add(T t)
        {
            _dbSet.Add(t);
            return t;
        }
        public async Task<T> Update(int id, T t)
        {

            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null || t == null)
            {
                return null;
            }
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(prop => prop.Name != "Id");

            foreach (var property in properties)
            {
                var updatedValue = property.GetValue(t);
                if (updatedValue != null)
                    property.SetValue(existingEntity, updatedValue);
            }
            return t;
        }
        public async Task<bool> Delete(int id)
        {
            if (_dbSet == null || _dbSet.Find(id) == null)
                return false;
            try
            {
                T t = await GetById(id);
                 _dbSet.Remove(t);

                return true;
            }
            catch { return false; }
        }
    }
}
