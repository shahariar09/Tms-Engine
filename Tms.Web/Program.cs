using System.Reflection;
using Tms.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();
app.UseRouting();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();


app.UseCors(x => x
    .AllowAnyMethod()           
    .AllowAnyHeader()           
    .SetIsOriginAllowed(origin => true) 
    .AllowCredentials());


app.MapControllers();

app.Run();
