using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tasks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<Settings>(hostContext.Configuration);
                    services.AddSingleton<IContextDbProvider, ContextDbProvider>();
                    services.AddSingleton<IDatabase, OracleDatabase>();
                    services.AddSingleton<IRepository, Repository>();
                    services.AddHostedService<HostedService>();
                })
                .Build()
                .Run();
        }
    }
}