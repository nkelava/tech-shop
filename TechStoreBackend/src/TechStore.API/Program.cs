using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Services;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Data.Seed;
using TechStore.Infrastructure.Repositories;
using TechStore.Infrastructure.Repositories.Base;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddTransient<DataSeeder>();
// Add project services
ConfigureServices(builder.Services);

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedData(app);

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
    ConfigureDatabase(services);
}

void ConfigureDatabase(IServiceCollection services)
{
    services.AddDbContext<TechStoreContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

void ConfigureApplicationLayer(IServiceCollection services)
{
    services.AddScoped<IBrandService, BrandService>();
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<INewsletterService, NewsletterService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IPropertyService, PropertyService>();
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
    services.AddScoped<INewsletterRepository, NewsletterRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IPropertyRepository, PropertyRepository>();
    services.AddScoped<IReviewRepository, ReviewRepository>();
    services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
    services.AddScoped<IWishlistRepository, WishlistRepository>();
}

async void SeedData(IHost app)
{
    if (args.Length == 1 && args[0].ToLower() == "seed:all")
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        using (var scope = scopedFactory?.CreateScope())
        {
            var service = scope?.ServiceProvider.GetService<DataSeeder>();

            if (service != null)
                await service.Seed();
        }
    }
}
