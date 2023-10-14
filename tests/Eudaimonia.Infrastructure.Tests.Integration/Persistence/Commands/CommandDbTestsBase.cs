using Eudaimonia.Infrastructure.Persistence.Commands;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands;

[Collection("CommandDatabase")]
public class CommandDbTestsBase : DbTestsBase<CommandDbContext>
{
    public CommandDbTestsBase(CommandDbFixture fixture)
        : base(fixture)
    {
    }
}