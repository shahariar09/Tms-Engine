//using System.Reflection;
//using Microsoft.EntityFrameworkCore;
//using Tms.Application.Mappings;
//using Tms.Domain.RepositoryAbstractions;
//using Tms.Infrastructure;
//using Tms.Infrastructure.Data.Repositories;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.PropertyNamingPolicy = null;
//    });

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddInfrastructureServices(builder.Configuration);

//builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
//builder.Services.AddScoped<IProjectAssignUserRepository, ProjectAssignUserRepository>();
//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    SeedData.Initialize(serviceProvider);
//}


//app.UseRouting();

////if (app.Environment.IsDevelopment())
////{
//    app.UseSwagger();
//    app.UseSwaggerUI();
////}

//app.UseHttpsRedirection();


//app.UseCors(x => x
//    .AllowAnyMethod()           
//    .AllowAnyHeader()           
//    .SetIsOriginAllowed(origin => true) 
//    .AllowCredentials());


//app.MapControllers();

//app.Run();
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tms.Application.Mappings;
using Tms.Domain.RepositoryAbstractions;
using Tms.Infrastructure;
using Tms.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add scoped services
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddScoped<IProjectAssignUserRepository, ProjectAssignUserRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        await SeedData.Initialize(serviceProvider);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
        throw; // Ensure the application stops if seeding fails
    }
}

// Configure the HTTP request pipeline
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.MapControllers();

app.Run();
