using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Tasks
{
    public class HostedService : BackgroundService
    {
        private readonly IRepository _repository;
        private readonly IContextDbProvider _contextDbProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<Settings> _settings;

        public HostedService(IOptions<Settings> settings, IServiceProvider serviceProvider,
            IRepository repository, IContextDbProvider contextDbProvider)
        {
            _settings = settings;
            _serviceProvider = serviceProvider;
            _repository = repository;
            _contextDbProvider = contextDbProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run( () =>
            {
                var tasksList = new List<Task>();

                var tnses = _settings.Value.Tns;
                foreach (var tns in tnses)
                    tasksList.Add(
                        Task.Factory.StartNew(() =>
                        {
                            _contextDbProvider.SetDb(new DbDetails {Tns = tns, Login = "", Password = ""});
                            
                            //using (var scope = _serviceProvider.CreateScope())
                            //{
                                //var repository = scope.ServiceProvider.GetService<IRepository>();
                                while (true)
                                {
                                    var dbName = _repository.GetDbName();
                                    Console.WriteLine($"Thread {tns} - select from db: {dbName}.");
                                    Console.WriteLine("");

                                    Thread.Sleep(3000);
                                }
                            //}
                        }, stoppingToken));

                while (true)
                {
                    foreach (var task in tasksList) Console.WriteLine(task.Status);

                    Thread.Sleep(1000);
                }
            }, stoppingToken);
        }
    }
}