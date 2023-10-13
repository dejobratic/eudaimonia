namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public interface IDatabaseContainer : IAsyncLifetime
{
    string GetConnectionString();
}