using Rira.GRPCS.Services;
using Rira.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.InitialConfig(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<UserGrpcService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
