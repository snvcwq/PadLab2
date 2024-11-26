using FoodDeliveryAPI.Entities;

namespace FoodDeliveryAPI.Interfaces
{
    public interface ISyncService<T> where T : BaseEntity
    {
        HttpResponseMessage Upsert(T record);
        HttpResponseMessage Delete(T record);
    }
}
