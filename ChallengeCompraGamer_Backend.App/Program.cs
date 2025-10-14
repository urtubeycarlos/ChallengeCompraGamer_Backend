using ChallengeCompraGamer_Backend.App;
using ChallengeCompraGamer_Backend.DataAccess.Context;
using ChallengeCompraGamer_Backend.Models.Maps;
using FluentValidation.AspNetCore;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;



WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.DisableDataAnnotationsValidation = true;
    });

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ChallengeCompraGamer_Backend API", Version = "v1" });
});

string connString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ChallengeCompraGamer_BackendContext' not found.");

builder.Services.AddDbContext<ChallengeCompraGamerContext>(options =>
            options.UseMySql(connString, ServerVersion.AutoDetect(connString),
            b => b.MigrationsAssembly("ChallengeCompraGamer_Backend.DataAccess")
       ));

LoggerConfig.Add(builder);

builder.Services.AddAutoMapper(typeof(MappingAssemblyMarker).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
