using Application;
using Carter;
using Domain.Abstractions;
using Domain.Orders;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderService.GrpcClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();
     
builder.Services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseLazyLoadingProxies();
    options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(ApplicationAssembly.Instance));

builder.Services.AddAutoMapper(ApplicationAssembly.Instance);

builder.Services.AddGrpcClient<ProductGrpcService.ProductGrpcServiceClient>( o => {
    o.Address = new Uri("https://localhost:7170");
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapCarter();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
