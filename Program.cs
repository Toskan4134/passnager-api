
using Microsoft.EntityFrameworkCore;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);

if (Environment.GetEnvironmentVariable("DB_PASSWORD") != null)
{
    builder.Configuration["DBPassword"] = Environment.GetEnvironmentVariable("DB_PASSWORD");
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conStrBuilder = new NpgsqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
conStrBuilder.Password = builder.Configuration["DBPassword"];
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(conStrBuilder.ConnectionString));

builder.Services.AddTransient<DataContext>();
builder.Services.AddTransient<IProfileService, ProfileService>();

builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "frontendOrigin",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5500").AllowAnyHeader().AllowAnyMethod();
                    });
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("frontendOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
