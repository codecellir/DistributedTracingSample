using Carter;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Students.BuildingBlock;
using Students.BuildingBlock.Evetns;
using Students.Reporting.Application;
using Students.Reporting.Application.Behaviours;
using Students.Reporting.Consumers;
using Students.Reporting.Persistence;
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
    configure.AddConsumer<StudentCreateConsumer>();
    configure.AddConsumer<StudentViewConsumer>();
    configure.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(ShareEnvironmentConfig.GetRabbitMQHost(), "/", h =>
        {
            h.Username(ShareEnvironmentConfig.GetRabbitMQUser());
            h.Password(ShareEnvironmentConfig.GetRABBITMQPassword());
        });
        cfg.ReceiveEndpoint(EventBusConstants.StudentReportingQueue, x =>
        {
            x.ConfigureConsumer<StudentCreateConsumer>(ctx);
            x.ConfigureConsumer<StudentViewConsumer>(ctx);
        });
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Student.Reporting.Api"))
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
