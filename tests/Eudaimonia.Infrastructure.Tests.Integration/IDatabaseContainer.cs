namespace Eudaimonia.Infrastructure.Tests.Integration;

public interface IDatabaseContainer : IAsyncLifetime
{
    string GetConnectionString();
}