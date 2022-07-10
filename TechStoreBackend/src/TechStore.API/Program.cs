using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Services;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories;
using TechStore.Infrastructure.Repositories.Base;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add project services
ConfigureServices(builder.Services);

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<TechStoreContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



void ConfigureServices(IServiceCollection services)
{
    ConfigureApplicationLayer(services);
    ConfigureInfrastructureLayer(services);
}


void ConfigureApplicationLayer(IServiceCollection services)
{
    services.AddScoped<IBrandService, BrandService>();
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IReviewService, ReviewService>();
    services.AddScoped<ISubcategoryService, SubcategoryService>();
    services.AddScoped<IWishlistService, WishlistService>();
}

void ConfigureInfrastructureLayer(IServiceCollection services)
{
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped(typeof(IRepositoryWrapper), typeof(RepositoryWrapper));
    services.AddScoped<IBrandRepository, BrandRepository>();
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IReviewRepository, ReviewRepository>();
    services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
    services.AddScoped<IWishlistRepository, WishlistRepository>();
}
