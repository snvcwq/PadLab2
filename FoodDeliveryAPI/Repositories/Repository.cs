using FoodDeliveryAPI.Context;
using FoodDeliveryAPI.Entities;
using FoodDeliveryAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FoodDeliveryContext _context;

        public Repository(FoodDeliveryContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAsync()
        {
            var collection = await _context.Set<T>().ToListAsync();
            return collection;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var existingEntity = await GetByIdAsync(entity.Id);

            if (existingEntity == null)
            {
                throw new Exception("Entity not found"); 
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
