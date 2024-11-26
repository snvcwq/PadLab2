using FoodDeliveryAPI.Entities;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<ICollection<T>> GetAsync();
        public Task AddAsync(T entity);
        public Task<T?> GetByIdAsync(Guid id);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
