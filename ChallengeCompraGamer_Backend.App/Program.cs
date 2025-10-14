using ChallengeCompraGamer_Backend.App;
using ChallengeCompraGamer_Backend.App.Middlewares;
using ChallengeCompraGamer_Backend.DataAccess.Context;
using ChallengeCompraGamer_Backend.Models.Maps;
using ChallengeCompraGamer_Backend.Services;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.DisableDataAnnotationsValidation = true;
    });

LoggerConfig.Add(builder);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

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

builder.Services.AddTransient<MicroService>();
builder.Services.AddTransient<ChoferService>();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();
