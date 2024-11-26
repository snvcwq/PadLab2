using Common;
using FoodDeliveryAPI.Entities;
using FoodDeliveryAPI.Interfaces;
using System.Text.Json;

namespace FoodDeliveryAPI.Services
{
    public class SyncService<T> : ISyncService<T> where T : BaseEntity
    {
        private readonly ISyncServiceSettings _settings;
        private readonly IHttpContextAccessor _httpContext;
        public SyncService(ISyncServiceSettings settings, IHttpContextAccessor httpContext)
        {
            _settings = settings;
            _httpContext = httpContext;
        }
        public HttpResponseMessage Delete(T record)
        {
            var syncType = _settings.DeleteHttpMethod;
            var json = ToSyncEntityJson(record, syncType);

            var response = HttpClientUtility.SendJson(json, _settings.Host, "POST");

            return response;
        }

        public HttpResponseMessage Upsert(T record)
        {
            var syncType = _settings.UpsertHttpMethod;
            var json = ToSyncEntityJson(record, syncType);

            var response = HttpClientUtility.SendJson(json, _settings.Host, "POST");

            return response;
        }

        private string ToSyncEntityJson(T record, string syncType)
        {
            var objectType = typeof(T);

            var syncEntity = new SyncEntity()
            {
                JsonData = JsonSerializer.Serialize(record),
                SyncType = syncType,
                ObjectType = objectType.Name,
                Id = record.Id,
                LastChangeAt = DateTime.Now,
                Origin = _httpContext.HttpContext.Request.Host.ToString()
            };

            var json = JsonSerializer.Serialize(syncEntity);

            return json;
        }
    }
}
