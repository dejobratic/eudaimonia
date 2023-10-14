namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence;

public interface IDbContainer : IAsyncLifetime
{
    string GetConnectionString();
}