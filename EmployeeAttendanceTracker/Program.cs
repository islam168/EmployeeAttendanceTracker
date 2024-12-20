using EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Interfaces;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.BLL.Services;
using EmployeeAttendanceTracker.EmployeeAttendanceTracker.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавление DbContext с использованием SQL Server
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

// Add Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICompanyService,  CompanyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
