using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TechStore.Domain.Entities;
using TechStore.Domain.Entities.Cart;
using TechStore.Domain.Entities.OrderAggregate;
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

                if (!_techStoreContext.Subcategories.Any())
                    await SeedSubcategories();

                if (!_techStoreContext.Attributes.Any())
                    await SeedAttributes();

                if (!_techStoreContext.AttributeValues.Any())
                    await SeedAttributeValues();

                if (!_techStoreContext.Products.Any())
                    await SeedProducts();

                if (!_techStoreContext.ProductAttributes.Any())
                    await SeedProductAttributes();

                if (!_techStoreContext.PromoCodes.Any())
                    await SeedPromoCodes();

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
                    Name = "Laptops",
                    Slug = "laptops",
                },
                new Category()
                {
                    Name = "Desktops",
                    Slug = "desktops",
                },
                new Category()
                {
                    Name = "Components",
                    Slug = "components",
                },
                new Category()
                {
                    Name = "Peripherals",
                    Slug = "peripherals",
                },
                new Category()
                {
                    Name = "Monitors",
                    Slug = "monitors",
                },
                new Category()
                {
                    Name = "Storage",
                    Slug = "storage",
                },
                new Category()
                {
                    Name = "Software",
                    Slug = "software",
                },
            };

            _techStoreContext.Categories.AddRange(categories);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedSubcategories()
        {
            var subcategories = new List<Subcategory>()
            {
                new Subcategory()
                {
                    Name = "Notebooks",
                    Slug = "notebooks",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("laptops")).First(),
                },
                new Subcategory()
                {
                    Name = "Ultrabooks",
                    Slug = "ultrabooks",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("laptops")).First(),
                },
                new Subcategory()
                {
                    Name = "MacBook",
                    Slug = "macbook",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("laptops")).First(),
                },
                new Subcategory()
                {
                    Name = "Gaming Laptops",
                    Slug = "gaming-laptops",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("laptops")).First(),
                },
                new Subcategory()
                {
                    Name = "Tower",
                    Slug = "tower",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("desktops")).First(),
                },
                new Subcategory()
                {
                    Name = "Compact",
                    Slug = "compact",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("desktops")).First(),
                },
                new Subcategory()
                {
                    Name = "All-in-one",
                    Slug = "all-in-one",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("desktops")).First(),
                },
                new Subcategory()
                {
                    Name = "Gaming Desktops",
                    Slug = "gaming-desktops",
                    Category = _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("desktops")).First(),
                },
                new Subcategory()
                {
                    Name = "CPU",
                    Slug = "cpu",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "GPU",
                    Slug = "gpu",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "RAM",
                    Slug = "ram",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "SSD",
                    Slug = "ssd",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "HDD",
                    Slug = "hdd",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Motherboards",
                    Slug = "motherboards",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Power Supply",
                    Slug = "power-supply",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Coolers",
                    Slug = "coolers",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Cases",
                    Slug = "cases",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "Thermal paste",
                    Slug = "thermal-paste",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("components")).First(),
                },
                new Subcategory()
                {
                    Name = "LED monitors",
                    Slug = "led",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("monitors")).First(),
                },
                new Subcategory()
                {
                    Name = "LCD monitors",
                    Slug = "lcd",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("monitors")).First(),
                },
                new Subcategory()
                {
                    Name = "Gaming Monitors",
                    Slug = "gaming-monitors",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("monitors")).First(),
                },
                new Subcategory()
                {
                    Name = "Keyboards",
                    Slug = "keyboards",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Mouses",
                    Slug = "mouses",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Mousepads",
                    Slug = "mousepads",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Headphones",
                    Slug = "headphones",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Speakers",
                    Slug = "speakers",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Microphones",
                    Slug = "microphones",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "Web cameras",
                    Slug = "web-cameras",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("peripherals")).First(),
                },
                new Subcategory()
                {
                    Name = "External SSD",
                    Slug = "external-ssd",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "External HDD",
                    Slug = "external-hdd",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "USB sticks",
                    Slug = "usb-sticks",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "Memory cards",
                    Slug = "memory-cards",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("storage")).First(),
                },
                new Subcategory()
                {
                    Name = "Operating systems",
                    Slug = "operating-systems",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("software")).First(),
                },
                new Subcategory()
                {
                    Name = "Antivirus",
                    Slug = "antivirus",
                    Category =  _techStoreContext.Categories.Where(c => c.Name.ToLower().Equals("software")).First(),
                },
            };

            _techStoreContext.Subcategories.AddRange(subcategories);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedAttributes()
        {
            var attributes = new List<ProductAttribute>()
            {
                new ProductAttribute()
                {
                    Name = "Manufacturer",
                },
                new ProductAttribute()
                {
                    Name = "Processor",
                },
                new ProductAttribute()
                {
                    Name = "Graphics Card",
                },
                new ProductAttribute()
                {
                    Name = "RAM",
                },
                new ProductAttribute()
                {
                    Name = "RAM Frequency",
                },
                new ProductAttribute()
                {
                    Name = "RAM Type",
                },
                new ProductAttribute()
                {
                    Name = "Display Size",
                },
                new ProductAttribute()
                {
                    Name = "Operating system",
                },
                new ProductAttribute()
                {
                    Name = "Language",
                },
                new ProductAttribute()
                {
                    Name = "Storage Type",
                },
                 new ProductAttribute()
                {
                    Name = "Cooling System",
                },
            };

            _techStoreContext.Attributes.AddRange(attributes);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedAttributeValues()
        {
            var attributeValues = new List<ProductAttributeValue>()
            {
                new ProductAttributeValue()
                {
                    Value = "4 GB",
                },
                new ProductAttributeValue()
                {
                    Value = "8 GB",
                },
                new ProductAttributeValue()
                {
                    Value = "16 GB",
                },
                 new ProductAttributeValue()
                {
                    Value = "32 GB",
                },
                new ProductAttributeValue()
                {
                    Value = "128 GB",
                },
                new ProductAttributeValue()
                {
                    Value = "256 GB",
                },
                new ProductAttributeValue()
                {
                    Value = "512 GB",
                },
                new ProductAttributeValue()
                {
                    Value = "1 TB",
                },
                new ProductAttributeValue()
                {
                    Value = "HDD",
                },
                new ProductAttributeValue()
                {
                    Value = "SSD",
                },
                new ProductAttributeValue()
                {
                    Value = "2400 MMz",
                },
                new ProductAttributeValue()
                {
                    Value = "2666 MMz",
                },
                new ProductAttributeValue()
                {
                    Value = "3200 MMz",
                },
                 new ProductAttributeValue()
                {
                    Value = "DDR3",
                },
                  new ProductAttributeValue()
                {
                    Value = "DDR4",
                },
                new ProductAttributeValue()
                {
                    Value = "Windows",
                },
                new ProductAttributeValue()
                {
                    Value = "Linux",
                },
                new ProductAttributeValue()
                {
                    Value = "Mac OS",
                },
                new ProductAttributeValue()
                {
                    Value = "Apple",
                },
                new ProductAttributeValue()
                {
                    Value = "Lenovo",
                },
                new ProductAttributeValue()
                {
                    Value = "Acer",
                },
                new ProductAttributeValue()
                {
                    Value = "Asus",
                },
                new ProductAttributeValue()
                {
                    Value = "AMD",
                },
                new ProductAttributeValue()
                {
                    Value = "NVidia",
                },
                new ProductAttributeValue()
                {
                    Value = "MSI",
                },
                new ProductAttributeValue()
                {
                    Value = "Intel",
                },
                new ProductAttributeValue()
                {
                    Value = "M1",
                },
                new ProductAttributeValue()
                {
                    Value = "Fan",
                },
                new ProductAttributeValue()
                {
                    Value = "14 inch",
                },
                new ProductAttributeValue()
                {
                    Value = "15,6 inch",
                },
                new ProductAttributeValue()
                {
                    Value = "17,3 inch",
                },

            };

            _techStoreContext.AttributeValues.AddRange(attributeValues);
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
                    ImageURL = "https://www.mikronis.hr/_shop/files/products/Helios300-bk.jpg?id=248",
                    Summary = "Acer Predator Helios 300, 15.6\" Full HD IPS, Intel i7 CPU, 16GB DDR4 RAM, 256GB SSD, GeForce GTX 1060, VR Ready, Red Backlit KB, Metal Chassis, Windows 10 64-bit, G3-571-77QK",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 1300,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 4.5m,
                    ReviewCount = 2,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Acer Aspire 7",
                    Slug = "acer-aspire-7",
                    ImageURL = "https://www.nabava.net/slike/products/10/58/21295810/acer-aspire-7-a715-42g-r403_7ed1eb8.jpeg",
                    Summary = "Acer Aspire 7, 15.6\" FHD IPS, AMD Ryzen 5 5500U up to 4.0GHz, 16GB DDR4, 512GB NVMe SSD, NVIDIA GeForce GTX1650 4GB, no OS",
                    Description = "Nenametljiv dizajn ovog prijenosnog računala skriva moćan procesor i grafiku, koji pomažu korisnicima da izvuku maksimum iz 15,6-inčnog zaslona s velikim omjerom zaslona i kućišta. Kao što biste i očekivali od prijenosnog računala ovog kalibra, opremljeno je i brzom Wi-Fi vezom te mnoštvom memorije i prostora za pohranu. Aspire 7 skriva mnogo snage u svom kućištu. Najnovija NVIDIA® grafika1 pruža svu potrebnu snagu za rad i igru, a Intel® Core™ procesor 10. generacije1 ili AMD Ryzen™ 5000 omogućuje da se sve odvija optimalnom brzinom. ",
                    Discount = 0,
                    OnSale = false,
                    Price = 779,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Acer Aspire 5",
                    Slug = "acer-aspire-5",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/l/a/laptop-acer-aspire-5-a515-45-r5x9-nxa7yex00a-ryzen-7-5700u-24gb-512gb-ssd-156-ips-noos-433xqryen-1155x1155.jpg",
                    Summary = "Acer Aspire 5, 15.6\" FHD IPS, AMD Ryzen 7 5700U up to 4.3GHz, 16GB DDR4, 512GB NVMe SSD, AMD Radeon Graphics, no OS",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 782,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Asus Vivobook 15",
                    Slug = "asus-vivobook-15",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/w/8/w800_1_2.png",
                    Summary = "Asus Vivobook 15, 15,6\", Intel Core i5, 8 GB, 512 GB, Windows 10 Home",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 749,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Asus Vivobook 16",
                    Slug = "asus-vivobook-16",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/z/t/ztfdrs.png",
                    Summary = "Acer Predator Helios 300 Gaming Laptop, 15.6\" Full HD IPS, Intel i7 CPU, 16GB DDR4 RAM, 256GB SSD, GeForce GTX 1060-6GB, VR Ready, Red Backlit KB, Metal Chassis, Windows 10 64-bit, G3-571-77QK",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 599,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Asus Vivobook Pro 16X",
                    Slug = "asus-vivobook-pro-16x",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/1/1/112_2_1.png",
                    Summary = "Asus Vivobook Pro 16X, 16,1\" 4K OLED, AMD Ryzen 9 6900HX 3.3GHz, 16GB DDR4, 512MB M.2 NVMe PCIe 4.0 SSD, NVIDIA GeForce RTX 3050 Ti, Windows 11 Home",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 1899,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Asus Vivobook X",
                    Slug = "asus-vivobook-x",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/5/5/550hzhz.png",
                    Summary = "Asus Vivobook X, 16\" WUXGA 60Hz, AMD Ryzen 5 5600H, 16GB DDR4, 1TB M.2 NVMe PCIe 3.0 SSD, Radeon Graphics, Windows 11 Home",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 1300,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Lenovo Ideapad 3",
                    Slug = "lenovo-ideapad-3",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/n/o/notebook-lenovo-ideapad-ultraslim-3-82rn-0001271198_1.jpg",
                    Summary = "Lenovo Ideapad 3, 15.6\" FHD IPS, AMD Ryzen 3 5425U up to 4.1GHz, 8GB DDR4, 512GB NVMe SSD, AMD Radeon Graphics, no OS",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 429,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Lenovo Ideapad 5",
                    Slug = "lenovo-ideapad-5",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/2/0/2022-11-07_113159.jpg",
                    Summary = "Lenovo Ideapad 5, 15.6\" FHD IPS, AMD Ryzen 5 5625U, 8GB DDR4, 512GB SSD M.2 2242 PCIe 3.0x4 NVMe, AMD Radeon Graphics, noOS",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 599,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Lenovo Ideapad Slim 3",
                    Slug = "lenovo-ideapad-slim-3",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/2/4/247315-wqq.png",
                    Summary = "Lenovo Ideapad Slim 3, 15,6\" FHD,AMD Ryzen 5-7520U 2,8GHz Core4, 16 GB, SSD 512GB M.2 PCIe NVMe, AMD Radeon 610M, FreeDOS",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 505,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Lenovo Yoga 7",
                    Slug = "Lenovo Yoga 7",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/u/l/ultrabook-aswyoga-7-82yl002f.png",
                    Summary = "Lenovo Yoga 7, 14\" 2.8 OLED Touch HDR500 90Hz, Intel Core i7 1360P up to 5.0GHz, 16GB DDR5, 1TB NVMe SSD, Intel Iris Xe Graphics, Win 11",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 1699,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
                },
                new Product
                {
                    Name = "Acer Aspire 3",
                    Slug = "Acer Aspire 3",
                    ImageURL = "https://cdn.sancta-domenica.hr/media/catalog/product/cache/f5919dfc2358b0859fbea71e815269bc/a/s/aspire-3-a317-53-non-sv-01_4.png",
                    Summary = "17.3\" FHD IPS, Intel Core i5 1135G7 up to 4.2GHz, 16GB DDR4, 512GB NVMe SSD, Intel Iris Xe Graphics, Win 11 Home",
                    Description = "Latest 7th Generation Intel Core i7 Processor 2.8GHz with Turbo Boost Technology up to 3.8GHz | Windows 10 Home 64-bit Latest NVIDIA GeForce GTX 1060 with 6 GB of dedicated GDDR5 VRAM 15.6\" Full HD (1920 x 1080) widescreen IPS display, Red Backlit Keyboard 16GB DDR4 DRAM Memory & 256GB SSD | Extra empty expandable hard drive slot for 2.5\" hard drives. Up to 7 - hours of battery life.",
                    Discount = 0,
                    OnSale = false,
                    Price = 759,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("notebooks")).First(),
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
                    Price = 1000,
                    UnitsInStock = 15,
                    UnitsSold = 5,
                    Rating = 4,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("gaming-laptops")).First(),
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
                    Price = 90,
                    UnitsInStock = 14,
                    UnitsSold = 5,
                    Rating = 4,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("ram")).First(),
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
                    Price = 150,
                    UnitsInStock = 8,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("ssd")).First(),
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
                    Price = 129,
                    UnitsInStock = 10,
                    UnitsSold = 0,
                    Rating = 0,
                    ReviewCount = 0,
                    Subcategory = _techStoreContext.Subcategories.Where(s => s.Slug.Equals("keyboards")).First(),
                },
            };

            _techStoreContext.Products.AddRange(products);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedProductAttributes()
        {
            var productAttributes = new List<ProductAttributeSet>()
            {
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("Manufacturer")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("Acer")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("Processor")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("Intel")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("Graphics Card")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("NVidia")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("RAM")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("8 GB")).First(),
                },
                 new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("RAM Frequency")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("2666 MMz")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("RAM Type")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("DDR4")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("Display Size")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("17,3 inch")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("Operating System")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("Windows")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("Storage Type")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("HDD")).First(),
                },
                new ProductAttributeSet
                {
                    Product = _techStoreContext.Products.Where(p => p.Slug.Equals("acer-predator-helios-300")).First(),
                    Attribute = _techStoreContext.Attributes.Where(a => a.Name.Equals("Cooling System")).First(),
                    AttributeValue = _techStoreContext.AttributeValues.Where(av => av.Value.Equals("Fan")).First(),
                }
            };

            _techStoreContext.ProductAttributes.AddRange(productAttributes);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedPromoCodes()
        {
            var promoCodes = new List<PromoCode>()
            {
                new PromoCode
                {
                    Code = "TP20",
                    Discount = 20,
                },
                new PromoCode
                {
                    Code = "TP30",
                    Discount = 30,
                },
                new PromoCode
                {
                    Code = "TP50",
                    Discount = 50,
                }
            };

            _techStoreContext.PromoCodes.AddRange(promoCodes);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedNewsletters()
        {
            var newsletters = new List<Newsletter>()
            {
                new Newsletter
                {
                    Email = "test@gmail.com",
                },
                new Newsletter
                {
                    Email = "subscriber@gmail.com",
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
                    Rate = 4,
                    Comment = "Very good!",
                    IsReported = false,
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                },
                new Review
                {
                    Email = "admin@gmail.com",
                    Rate = 5,
                    Comment = "Not good! Wouldn't recommend.",
                    IsReported = false,
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
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
            };

            _techStoreContext.WishLists.AddRange(wishlists);
            await _techStoreContext.SaveChangesAsync();
        }

        private async Task SeedWishlistProducts()
        {
            var wishlistProducts = new List<WishlistProduct>()
            {
                new WishlistProduct
                {
                    Wishlist = _techStoreContext.WishLists.Where(w => w.Email.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
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
                    Email = "test@gmail.com",
                    TotalPrice = 1210,
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
                    Cart = _techStoreContext.Carts.Where(c => c.Email.ToLower().Equals("test@gmail.com")).First(),
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                    Quantity = 1,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First().Price,
                    TotalPrice = 1 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First().Price,
                }
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
                    Country = "Croatia",
                    City = "Split",
                    ShippingAddress = "Ul. Test 127",
                    ZipCode = 21000,
                    TotalPrice = 1210,
                    Status = OrderStatus.Completed,
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
                    Product = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First(),
                    Quantity = 1,
                    UnitPrice = _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First().Price,
                    TotalPrice = 1 * _techStoreContext.Products.Where(p => p.Slug.ToLower().Equals("acer-predator-helios-300")).First().Price,
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
                {
                    await roleStore.CreateAsync(
                        new IdentityRole
                        {
                            Name = role,
                            NormalizedName = role.ToUpper()
                        });
                }
            }
        }
    }
}
