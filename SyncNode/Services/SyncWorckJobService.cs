using Common;
using SyncNode.Settings;
using System.Collections.Concurrent;

namespace SyncNode.Services
{
    public class SyncWorckJobService : IHostedService
    {
        private readonly ConcurrentDictionary<Guid, SyncEntity> documents = new();

        private readonly IUserAPISettings _userAPISettings;
        private Timer _timer;

        public SyncWorckJobService(IUserAPISettings userAPISettings)
        {
            _userAPISettings = userAPISettings;
        }

        public void AddItem(SyncEntity entity)
        {
            SyncEntity? document = null;

            bool isPresent = documents.TryGetValue(entity.Id, out document);

            if (!isPresent || (isPresent && entity.LastChangeAt > document.LastChangeAt))
            {
                documents[entity.Id] = entity;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoSendWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        private void DoSendWork(object state)
        {
            foreach (var doc in documents)
            {
                SyncEntity? entity = null;

                var isPressent = documents.TryRemove(doc.Key, out entity);

                if (isPressent)
                {
                    var recivers = _userAPISettings.Hosts.Where(x => !x.Contains(entity.Origin));

                    foreach (var reciver in recivers)
                    {
                        var url = $"{reciver}/{entity.ObjectType}/sync";

                        try
                        {
                            var result = HttpClientUtility.SendJson(entity.JsonData, url, entity.SyncType);

                            if (!result.IsSuccessStatusCode)
                            {

                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }
    }
}
