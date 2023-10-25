using Eudaimonia.Infrastructure;
using Eudaimonia.Infrastructure.Persistence.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection()
    .AddDatabase(configuration)
    .AddSingleton<IConfiguration>(configuration)
    .BuildServiceProvider();

var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<CommandDbContext>();
await context.Database.MigrateAsync();
