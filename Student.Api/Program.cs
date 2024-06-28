using Carter;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ParisanTruvel.Core.Infrastructure.Behaviours;
using Students.Api.Application;
using Students.Api.Persistence;
using Students.BuildingBlock;
using Students.BuildingBlock.Evetns;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(EnvironmentConfig.GetConnectionString(builder)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
         .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddCarter();


builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(ShareEnvironmentConfig.GetRabbitMQHost(), "/", h =>
        {
            h.Username(ShareEnvironmentConfig.GetRabbitMQUser());
            h.Password(ShareEnvironmentConfig.GetRABBITMQPassword());
        });
    });
    configure.AddConsumers(typeof(BaseEvent).Assembly);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Student.Api"))
    .WithTracing(tracing =>
    {
        tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSqlClientInstrumentation(d => d.SetDbStatementForText = true)
        .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

        tracing.AddOtlpExporter();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(options => { });

app.MapCarter();


using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await context?.Database.MigrateAsync()!;

app.Run();

