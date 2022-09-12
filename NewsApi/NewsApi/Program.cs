using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using NewsApi.Data;
using NewsApi.Profiles;
using NewsApi.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NewsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'WebApiContext' not found.")));

builder.Services.AddScoped<INewsService, NewsService>();
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<NewsApi.Filters.ErrorHandlingFilterAttribute>());

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new NewsDtoProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<WebApi.Middleware.ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
