using Eudaimonia.Api;
using Eudaimonia.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProjectDependencies(builder.Configuration);

var app = builder.Build().ConfigurePresentation();

app.Run();