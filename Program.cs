using System.Net;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);


if (Environment.GetEnvironmentVariable("DB_PASSWORD") != null)
{
    builder.Configuration["DBPassword"] = Environment.GetEnvironmentVariable("DB_PASSWORD");
}
if (Environment.GetEnvironmentVariable("DB_CONN_STRING") != null)
{
    builder.Configuration["ConnectionString"] = Environment.GetEnvironmentVariable("DB_CONN_STRING");
}
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conStrBuilder = new NpgsqlConnectionStringBuilder(builder.Configuration["ConnectionString"]);
conStrBuilder.Password = builder.Configuration["DBPassword"];
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(conStrBuilder.ConnectionString));

builder.Services.AddTransient<DataContext>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<ISiteService, SiteService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "frontendOrigin",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
builder.Services.AddMvc().AddMvcOptions(e => e.EnableEndpointRouting = false);


if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
        options.HttpsPort = 5107;
    });
}
//Logger
Logger logger = new Logger("./logs");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("frontendOrigin");
app.UseMvc();
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();