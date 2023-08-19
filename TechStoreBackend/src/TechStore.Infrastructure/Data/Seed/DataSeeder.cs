using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TechStore.Domain.Entities;
using TechStore.Domain.Entities.Cart;
using TechStore.Domain.Entities.Order;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Domain.Entities.User;
using TechStore.Domain.Entities.Wishlist;
using TechStore.Domain.Enums.Order;

namespace TechStore.Infrastructure.Data.Seed
{
    public class DataSeeder
    {
        private readonly TechStoreContext _techStoreContext;
        private readonly int _retryMax = 5;

        public DataSeeder(TechStoreContext techStoreContext)
        {
            _techStoreContext = techStoreContext;
        }

        public async Task Seed(int retry = 0)
        {
            try
            {
                if (!_techStoreContext.Categories.Any())
                    await SeedCategories();

                if (!_techStoreContext.Properties.Any())
                    await SeedProperties();

                if (!_techStoreContext.Subcategories.Any())
                    await SeedSubcategories();

                if (!_techStoreContext.SubcategoryProperties.Any())
                    await SeedSubcategoryProperties();

                if (!_techStoreContext.Products.Any())
                    await SeedProducts();

                if (!_techStoreContext.ProductProperties.Any())
                    await SeedProductProperties();

                if (!_techStoreContext.Newsletters.Any())
                    await SeedNewsletters();

                if (!_techStoreContext.Reviews.Any())
                    await SeedReviews();

                if (!_techStoreContext.WishLists.Any())
                    await SeedWishlists();

                if (!_techStoreContext.WishListProducts.Any())
                    await SeedWishlistProducts();

                if (!_techStoreContext.Carts.Any())
                    await SeedCarts();

                if (!_techStoreContext.CartProducts.Any())
                    await SeedCartProducts();

                if (!_techStoreContext.Orders.Any())
                    await SeedOrders();

                if (!_techStoreContext.OrderProducts.Any())
                    await SeedOrderProducts();

                if (!_techStoreContext.UserRoles.Any())
                    await SeedRoles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                if(retry < _retryMax)
                {
                    await Seed(++retry);
                }
            }
        }

        private async Task SeedCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Computers",
                    Slug = "computers",
                },
                new Category()
                {
                    Name = "Components",
                    Slug = "components",
                },
                new Category()
                {
                    Name = "Monitors",
                    Slug = "monitors",
                },
                new Category()
                {
                    Name = "Peripherals",
                    Slug = "peripherals",
                },
                new Category()
                {
                    Name = "Storage",
                    Slug = "storage",

                },
                new Category()
                {
                    Name = "Software",
                    Slug = "software"
                },
            };

            _techStoreContext.Categories.AddRange(categories);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedProperties()
        {
            var properties = new List<Property>()
            {
                new Property()
                {
                    Name = "Capacity",
                    ValueType = "number",
                },
                new Property()
                {
                    Name = "Frequency",
                    ValueType = "number",
                },
                 new Property()
                {
                    Name = "Type",
                    ValueType = "text",
                },
                new Property()
                {
                    Name = "Latency",
                    ValueType = "text",
                },
                new Property()
                {
                    Name = "Module count",
                    ValueType = "number",
                },
                new Property()
                {
                    Name = "Processor",
                    ValueType = "text",
                },
                new Property()
                {
                    Name = "Cores",
                    ValueType = "number",
                },
                new Property()
                {
                    Name = "Display Size",
                    ValueType = "text",
                },
                new Property()
                {
                    Name = "Resolution",
                    ValueType = "text",
                },
                new Property()
                {
                    Name = "Operating system",
                    ValueType = "text",
                },
                new Property()
                {
                    Name = "Language",
                    ValueType = "text",
                },
                new Property()
                {
                    Name = "Color",
                    ValueType = "text",
                },
            };

            _techStoreContext.Properties.AddRange(properties);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedSubcategories()
        {
            var subcategories = new List<Subcategory>()
            {
                new Subcategory()
                {
                    Name = "Laptops",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("computers")).First(),
                },
                new Subcategory()
                {
                    Name = "PCs",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("computers")).First(),
                },
                new Subcategory()
                {
                    Name = "Gaming laptops",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("computers")).First(),
                },
                new Subcategory()
                {
                    Name = "Gaming PCs",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("computers")).First(),
                },
                new Subcategory()
                {
                    Name = "CPU",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "GPU",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "RAM",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "SSD",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "HDD",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Motherboard",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Power Supply",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Coolers",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Cases",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Thermal paste",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "LED monitors",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("monitors")).First(),
                },
                new Subcategory()
                {
                    Name = "LCD monitors",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("monitors")).First(),
                },
                new Subcategory()
                {
                    Name = "Gaming monitors",
                   Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("monitors")).First(),
                },
                new Subcategory()
                {
                    Name = "Keyboards",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Mouses",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Mousepads",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Headphones",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Speakers",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Microphones",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Web cameras",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "External SSD",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "External HDD",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "USB sticks",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "Memory cards",
                   Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "Operating system",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("software")).First(),
                },
                new Subcategory()
                {
                    Name = "Applications",
                   Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("software")).First(),
                },
                new Subcategory()
                {
                    Name = "Antivirus",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("software")).First(),
                },
            };

            _techStoreContext.Subcategories.AddRange(subcategories);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedSubcategoryProperties()
        {
            var subcategoryProperties = new List<SubcategoryProperty>()
            {
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("laptops")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("processor")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("laptops")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("resolution")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("pcs")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("color")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("pcs")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("operating system")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("gaming laptops")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("resolution")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("gaming pcs")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("operating system")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Contains("cpu")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("frequency")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Contains("gpu")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("capacity")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Contains("ram")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("frequency")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Contains("ssd")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("capacity")).First(),
                },
                new SubcategoryProperty
                {
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Contains("hdd")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("capacity")).First(),
                },
            };

            _techStoreContext.SubcategoryProperties.AddRange(subcategoryProperties);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedProducts()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Acer Predator Helios 300",
                    Slug = "acer-predator-helios-300",
                    ImageURL = "https://www.links.hr/content/images/thumbs/013/0132409_prijenosno-racunalo-acer-predator-helios-300-nh-qb7ex-00b-core-i7-11800h-16gb-512gb-ssd-geforce-rtx.png",
                    Summary = "Acer Predator Helios 300 Gaming Laptop, 15.6\" Full HD IPS, Intel i7 CPU, 16GB DDR4 RAM, 256GB SSD, GeForce GTX 1060-6GB, VR Ready, Red Backlit KB, Metal Chassis, Windows 10 64-bit, G3-571-77QK",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    SalePrice = 0,
                    Price = 1300,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Brand = _techStoreContext.Brands.Where(b => b.Name.ToLower().Equals("acer")).First(),
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("laptops")).First(),
                },
                new Product
                {
                    Name = "Dell Inspiron 17 3793",
                    Slug = "dell-inspiron-17-3793",
                    ImageURL = "https://yekupi.blob.core.windows.net/ekupihr/515Wx515H/EK000338374_1.image",
                    Summary = "Dell Inspiron 17 3793 Laptop 17.3\" Full HD,10th Gen Intel i5-1035G1, 8GB RAM, 512GB SSD, Windows 10",
                    Description = "17. 3-inch Full HD (1920 x 1080) Anti-Glare LED-Backlit Non-touch WVA Display Intel UHD Graphics 10th Generation Intel Core i5-1035G1 Processor, 6MB Cache, up to 3. 60 GHz, 8GB DDR4 Ram, 512GB M. 2 PCIe NVMe Solid State Drive SD Card Reader, USB 2. 0, Optical Disk Drive, USB 3. 1 Type-C, HDMI 1. 4b, RJ45, (2) USB 3. 1 Gen 1, Windows 10, 1 Year, Headphone & Microphone Audio Jack",
                    Discount = 10,
                    OnSale = true,
                    SalePrice = 900,
                    Price = 1000,
                    UnitsInStock = 15,
                    UnitsSold = 5,
                    Rating = 4,
                    ReviewCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Brand = _techStoreContext.Brands.Where(b => b.Name.ToLower().Equals("dell")).First(),
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("gaming laptops")).First(),
                },
                new Product
                {
                    Name = "Kingston FURY Impact",
                    Slug = "kingston-fury-impact-16",
                    ImageURL = "https://static10.edstatic.net/product_images/470x470/resize/01_u00if465.jpg?v=1",
                    Summary = "Kingston FURY Impact 16GB 2666MHz DDR4 CL15 Laptop Memory Single Stick KF426S16IB/16, Black ",
                    Description = " Maximize your memory and get a boost to your gaming, multitasking, and rendering. Plug N Play Automatic Overclocking Functionality - automatically overclocks to the highest published frequency. Intel XMP-Ready Profiles. Ready for AMD Ryzen. Higher Performance with Low Power Consumption - Low 1.2V power draw to run your system efficiently.",
                    Discount = 0,
                    OnSale = false,
                    SalePrice = 0,
                    Price = 90,
                    UnitsInStock = 14,
                    UnitsSold = 5,
                    Rating = 4,
                    ReviewCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Brand = _techStoreContext.Brands.Where(b => b.Name.ToLower().Equals("kingston")).First(),
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("ram")).First(),
                },
                new Product
                {
                    Name = "Samsung 970 Evo",
                    Slug = "samsung-970-evo",
                    ImageURL = "https://www.links.hr/content/images/thumbs/006/0061144_ssd-250-0-gb-samsung-970-evo-plus-nvme-m-2-mz-v7s250bw-maks-do-3500-2300-mb-s-051400632.jpg",
                    Summary = "SAMSUNG 970 EVO 250GB - NVMe PCIe M.2 2280 SSD (MZ-V7E250BW)",
                    Description = "Built with Samsung’s industry leading V-NAND technology for reliable and superior performance Read speeds up to 3,500MB/s* with a 5-year limited warranty and exceptional endurance up to 1,200 TBW* (* May vary by capacity) Seamless cloning and file transfers with the Samsung Magician Software, the ideal SSD management solution for performance optimization and data security with automatic firmware updates Samsung’s Dynamic Thermal Guard reduces risk of overheating and minimizes performance drop. The NVMe interface (PCIe M.2 2280) offers enhanced bandwidth, low latency, and power efficiency, perfect for tech enthusiasts, high-end gamers, and 4K & 3D content designers.Power consumption (Idle):Max. 30 mW. Product of Korea, manufactured by China.",
                    Discount = 0,
                    OnSale = false,
                    SalePrice = 0,
                    Price = 150,
                    UnitsInStock = 8,
                    UnitsSold = 1,
                    Rating = 4.5m,
                    ReviewCount = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Brand = _techStoreContext.Brands.Where(b => b.Name.ToLower().Equals("samsung")).First(),
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("ssd")).First(),
                },
                new Product
                {
                    Name = "Apple Magic Keyboard",
                    Slug = "apple-magic-keyboard",
                    ImageURL = "https://istyle.hr/media/catalog/product/cache/image/700x700/e9c3970ab036de70892d86c6d221abfe/m/k/mk2a3b_1.jpeg",
                    Summary = "Apple Magic Keyboard - US English",
                    Description = "Magic Keyboard combines a sleek design with a built-in rechargeable battery and enhanced key features. With a stable scissor mechanism beneath each key, as well as optimized key travel and a low profile, Magic Keyboard provides a remarkably comfortable and precise typing experience. It pairs automatically with your Mac, so you can get to work right away. And the battery is incredibly long-lasting—it will power your keyboard for about a month or more between charges. System Requirements: Bluetooth - enabled Mac computer with OS X 10.11 or later, iPad models with iPadOS, iOS devices with iOS 9.1 or later",
                    Discount = 0,
                    OnSale = false,
                    SalePrice = 0,
                    Price = 129,
                    UnitsInStock = 10,
                    UnitsSold = 3,
                    Rating = 1.8m,
                    ReviewCount = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Brand = _techStoreContext.Brands.Where(b => b.Name.ToLower().Equals("apple")).First(),
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Name.ToLower().Equals("keyboards")).First(),
                },
            };

            _techStoreContext.Products.AddRange(products);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedProductProperties()
        {
            var productProperties = new List<ProductProperty>()
            {
                new ProductProperty
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("capacity")).First(),
                    Value = "500",
                },
                new ProductProperty
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("processor")).First(),
                    Value = "3.2",
                },
                new ProductProperty
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("display size")).First(),
                    Value = "17.3",
                },
                new ProductProperty
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                    Property = _techStoreContext.Properties.Where(p => p.Name.ToLower().Equals("resolution")).First(),
                    Value = "1920x1080",
                },
            };

            _techStoreContext.ProductProperties.AddRange(productProperties);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedNewsletters()
        {
            var newsletters = new List<Newsletter>()
            {
                new Newsletter
                {
                    Email = "test@gmail.com",
                    CreatedAt = DateTime.Now,
                },
                new Newsletter
                {
                    Email = "subscriber@gmail.com",
                    CreatedAt = DateTime.Now,
                },
            };

            _techStoreContext.Newsletters.AddRange(newsletters);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedReviews()
        {
            var reviews = new List<Review>()
            {
                new Review
                {
                    Email = "test@gmail.com",
                    Rate = 4.5m,
                    Comment = "Very good!",
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First(),
                    CreatedAt = DateTime.Now,
                },
                new Review
                {
                    Email = "admin@gmail.com",
                    Rate = 1.8m,
                    Comment = "Not good! Wouldn't recommend.",
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("apple-magic-keyboard")).First(),
                    CreatedAt = DateTime.Now,
                },
            };

            _techStoreContext.Reviews.AddRange(reviews);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedWishlists()
        {
            var wishlists = new List<Wishlist>()
            {
                new Wishlist
                {
                    Email = "test@gmail.com",
                },
                new Wishlist
                {
                    Email = "admin@gmail.com",
                },
            };

            _techStoreContext.WishLists.AddRange(wishlists);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedWishlistProducts()
        {
            var wishlistProducts = new List<WishListProduct>()
            {
                new WishListProduct
                {
                    WishList = _techStoreContext.WishLists.Where(w => w.Email.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                },
                new WishListProduct
                {
                    WishList = _techStoreContext.WishLists.Where(w => w.Email.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First(),
                },
                new WishListProduct
                {
                    WishList = _techStoreContext.WishLists.Where(w => w.Email.ToLower().Equals("admin@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First(),
                },
            };

            _techStoreContext.WishListProducts.AddRange(wishlistProducts);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedCarts()
        {
            var carts = new List<Cart>()
            {
                new Cart
                {
                    Username = "test@gmail.com",
                    TotalPrice = 1210,
                },
                new Cart
                {
                    Username = "admin@gmail.com",
                    TotalPrice = 1480,
                },
            };

            _techStoreContext.Carts.AddRange(carts);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedCartProducts()
        {
            var cartProducts = new List<CartProduct>()
            {
                new CartProduct
                {
                    Cart = _techStoreContext.Carts.Where(c => c.Username.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First(),
                    Quantity = 1,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First().Price,
                    TotalPrice = 1 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First().Price,
                },
                new CartProduct
                {
                    Cart = _techStoreContext.Carts.Where(c => c.Username.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("dell-inspiron-17-3793")).First(),
                    Quantity = 1,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("dell-inspiron-17-3793")).First().Price,
                    TotalPrice = 1 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("dell-inspiron-17-3793")).First().Price,
                },
                new CartProduct
                {
                    Cart = _techStoreContext.Carts.Where(c => c.Username.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First(),
                    Quantity = 2,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First().Price,
                    TotalPrice = 2 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First().Price,
                },
                new CartProduct
                {
                    Cart = _techStoreContext.Carts.Where(c => c.Username.ToLower().Equals("admin@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                    Quantity = 1,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First().Price,
                    TotalPrice = 1 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First().Price,
                },
                new CartProduct
                {
                    Cart = _techStoreContext.Carts.Where(c => c.Username.ToLower().Equals("admin@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First(),
                    Quantity = 2,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First().Price,
                    TotalPrice = 2 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First().Price,
                },
            };

            _techStoreContext.CartProducts.AddRange(cartProducts);
            await _techStoreContext.SaveChangesAsync();
        }
        private async Task SeedOrders()
        {
            var orders = new List<Order>()
            {
                new Order
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "test@gmail.com",
                    ContactNumber = "0991234885",
                    Country = "New Test",
                    City = "Test City",
                    ShippingAddress = "Ul. Test 127",
                    PostalCode = 12700,
                    TotalPrice = 1210,
                    Status = OrderStatus.Completed,
                    PaymentMethod = PaymentMethod.Cash,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
            };

            _techStoreContext.Orders.AddRange(orders);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedOrderProducts()
        {
            var orderProducts = new List<OrderProduct>()
            {
               new OrderProduct
                {
                    Order = _techStoreContext.Orders.Where(o => o.Email.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First(),
                    Quantity = 1,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First().Price,
                    TotalPrice = 1 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("samsung-970-evo")).First().Price,
                },
                new OrderProduct
                {
                    Order = _techStoreContext.Orders.Where(o => o.Email.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("dell-inspiron-17-3793")).First(),
                    Quantity = 1,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("dell-inspiron-17-3793")).First().Price,
                    TotalPrice = 1 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("dell-inspiron-17-3793")).First().Price,
                },
                new OrderProduct
                {
                    Order = _techStoreContext.Orders.Where(o => o.Email.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First(),
                    Quantity = 2,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First().Price,
                    TotalPrice = 2 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("kingston-fury-impact-16")).First().Price,
                },
            };

            _techStoreContext.OrderProducts.AddRange(orderProducts);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(_techStoreContext);
            var userRoles = new List<string> { UserRoles.Admin, UserRoles.User };

            foreach(var role in userRoles)
            {
                if (!_techStoreContext.Roles.Any(r => r.Name.ToLower().Equals(role.ToLower())))
                    await roleStore.CreateAsync(new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    });
            }
        }
    }
}
