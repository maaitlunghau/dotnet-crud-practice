using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Middleware;
using server.Repositories;
using server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQL"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQL")))
);
builder.Services.AddScoped<ITaskRepository, TaskService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
