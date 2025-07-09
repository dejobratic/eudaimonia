using Eudaimonia.Infrastructure.Persistence.Commands;

namespace Eudaimonia.Infrastructure.Tests.Integration.Persistence.Commands;

[Collection("CommandDatabase")]
public class CommandDbTestsBase(CommandDbFixture fixture) : DbTestsBase<CommandDbContext>(fixture);