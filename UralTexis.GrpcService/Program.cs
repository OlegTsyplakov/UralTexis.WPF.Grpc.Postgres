using Microsoft.EntityFrameworkCore;
using UralTexis.Grpc.Services;
using UralTexis.Postgres;
using UralTexis.Postgres.Interfaces;
using UralTexis.Postgres.Repository;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();
var connectionString = configuration.GetSection("DataBaseSettings:ConnectionString");
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddDbContext<AppDbContext>(options =>
                             options.UseNpgsql(connectionString.Value));


var app = builder.Build();

app.MapGrpcService<WorkerService>();
app.MapGrpcReflectionService();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
