using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stripe;
using TechStore.API.Configuration;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Services;
using TechStore.Domain.Entities.User;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Data.Seed;
using TechStore.Infrastructure.Repositories;
using TechStore.Infrastructure.Repositories.Base;
using TechStore.Infrastructure.Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add project services
ConfigureServices(builder.Services);

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials(); ;
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedData(app);

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    ConfigureHttpContextAccessor(services);
    ConfigureIdentity(services);
    ConfigureAuthentication(services);
    ConfigureDatabase(services);
    ConfigureSeeder(services);
    ConfigureApplicationLayer(services);
    ConfigureInfrastructureLayer(services);
    ConfigureStripe(services);
}

void ConfigureDatabase(IServiceCollection services)
{
    services.AddDbContext<TechStoreContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

void ConfigureSeeder(IServiceCollection services)
{
    services.AddTransient<DataSeeder>();
}

void ConfigureStripe(IServiceCollection services)
{
    services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
}

void ConfigureHttpContextAccessor(IServiceCollection services)
{
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}

void ConfigureIdentity(IServiceCollection services)
{
    services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<TechStoreContext>();

    services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
    });
}

void ConfigureAuthentication(IServiceCollection services)
{
    services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtSettings:SecretKey").Value);
    var tokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value,
        RequireExpirationTime = false, // for dev purposes
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
        jwt.SaveToken = true;
        jwt.RequireHttpsMetadata = false;
        jwt.TokenValidationParameters = tokenValidationParameters; 
    });

    services.AddSingleton(tokenValidationParameters);
}

void ConfigureApplicationLayer(IServiceCollection services)
{
    services.AddScoped<IAttributeService, AttributeService>();
    services.AddScoped<IAttributeValueService, AttributeValueService>();
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<INewsletterService, NewsletterService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<IProductService, TechStore.Application.Services.ProductService>();
    services.AddScoped<IPromoCodeService, PromoCodeService>();
    services.AddScoped<IReviewService, TechStore.Application.Services.ReviewService>();
    services.AddScoped<ISubcategoryService, SubcategoryService>();
    services.AddScoped<IWishlistService, WishlistService>();
}

void ConfigureInfrastructureLayer(IServiceCollection services)
{
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped(typeof(IRepositoryWrapper), typeof(RepositoryWrapper));
    services.AddScoped<IAttributeRepository, AttributeRepository>();
    services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<INewsletterRepository, NewsletterRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IProductAttributeSetRepository, ProductAttributeSetRepository>();
    services.AddScoped<IReviewRepository, ReviewRepository>();
    services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
    services.AddScoped<IWishlistRepository, WishlistRepository>();
}

async void SeedData(IHost app)
{
    if (args.Length == 1 && args[0].ToLower() == "seed:all")
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        using var scope = scopedFactory?.CreateScope();
        var service = scope?.ServiceProvider.GetService<DataSeeder>();

        if (service != null)
            await service.Seed();
    }
}
