using Microsoft.EntityFrameworkCore;
using project_ecommerce_api.Models;
using project_ecommerce_api.Repositories.Interface;
using project_ecommerce_api.Repositories.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Repositories
// Product
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IProductColor, ProductColorService>();
builder.Services.AddScoped<IProductOption, ProductOptionService>();
builder.Services.AddScoped<IProductShop, ProductShopService>();
builder.Services.AddScoped<IProperty, PropertyService>();
builder.Services.AddScoped<IProductImage, ProductImageService>();

builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IBrand, BrandService>();
builder.Services.AddScoped<IAddressShop, AddressShopService>();
// Customer
builder.Services.AddScoped<ICustomer, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
