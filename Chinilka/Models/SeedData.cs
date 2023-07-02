using Chinilka.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinilka.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ChinilkaDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ChinilkaDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            AddCategories(context);

            AddDeviceModels(context);

            AddTestProducts(context);
        }

        private static void AddCategories(ChinilkaDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "iPhone" },
                    new Category { Name = "iPad" },
                    new Category { Name = "iMac" },
                    new Category { Name = "Macbook" },
                    new Category { Name = "AppleWatch" },
                    new Category { Name = "Android" },
                    new Category { Name = "Аксессуары" });

                context.SaveChanges();
            }
        }
        private static void AddDeviceModels(ChinilkaDbContext context)
        {
            if (!context.DeviceModels.Any())
            {
                var categoties = context.Categories.ToList();
                var iPhone = categoties.First(_ => _.Name == "iPhone");
                var iPad = categoties.First(_ => _.Name == "iPad");
                var iMac = categoties.First(_ => _.Name == "iMac");
                var macbook = categoties.First(_ => _.Name == "Macbook");
                var appleWatch = categoties.First(_ => _.Name == "AppleWatch");
                var android = categoties.First(_ => _.Name == "Android");
                var accessories = categoties.First(_ => _.Name == "Аксессуары");

                context.DeviceModels.AddRange(
                    new DeviceModel { Name = "iPhone 14", Category = iPhone, ImagePath = @"/images/iPhone/iPhone14.png" },
                    new DeviceModel { Name = "iPhone 14 Plus", Category = iPhone, ImagePath = @"/images/iPhone/iPhone14Plus.png" },
                    new DeviceModel { Name = "iPhone 14 Pro", Category = iPhone, ImagePath = @"/images/iPhone/iPhone14Plus.png" },
                    new DeviceModel { Name = "iPhone 14 Pro Max", Category = iPhone, ImagePath = @"/images/iPhone/iPhone14ProMax.png" },
                    new DeviceModel { Name = "iPhone 13", Category = iPhone, ImagePath = @"/images/iPhone/iPhone13.png" },
                    new DeviceModel { Name = "iPhone 13 Mini", Category = iPhone, ImagePath = @"/images/iPhone/iPhone13Mini.png" },
                    new DeviceModel { Name = "iPhone 13 Pro", Category = iPhone, ImagePath = @"/images/iPhone/iPhone13Pro.png" },
                    new DeviceModel { Name = "iPhone 13 Pro Max", Category = iPhone, ImagePath = @"/images/iPhone/iPhone13ProMax.png" },
                    new DeviceModel { Name = "iPhone 12", Category = iPhone, ImagePath = @"/images/iPhone/iPhone12.png" },
                    new DeviceModel { Name = "iPhone 11", Category = iPhone, ImagePath = @"/images/iPhone/iPhone11.png" },
                    new DeviceModel { Name = "iPhone SE 2022", Category = iPhone, ImagePath = @"/images/iPhone/iPhone14Plus.png" },
                    new DeviceModel { Name = "iPhone XR", Category = iPhone, ImagePath = @"/images/iPhone/iPhoneXR.png" });

                context.DeviceModels.AddRange(
                    new DeviceModel { Name = "iPad Air 10.9 (2022)", Category = iPad, ImagePath = @"/images/iPad/iPadAir10.9(2022).png" },
                    new DeviceModel { Name = "iPad 9 (2021)", Category = iPad, ImagePath = @"/images/iPad/iPad9(2021).png" },
                    new DeviceModel { Name = "iPad Pro 11 (2022)", Category = iPad, ImagePath = @"/images/iPad/iPadPro11(2022).png" },
                    new DeviceModel { Name = "iPad Pro 12.9 (2022)", Category = iPad, ImagePath = @"/images/iPad/iPadPro12.9(2022).png" },
                    new DeviceModel { Name = "iPad 10 (2022)", Category = iPad, ImagePath = @"/images/iPad/iPad10(2022).png" },
                    new DeviceModel { Name = "iPad mini 6 (2021)", Category = iPad, ImagePath = @"/images/iPad/iPadmini6(2021).png" },
                    new DeviceModel { Name = "iPad Pro 11 (2021)", Category = iPad, ImagePath = @"/images/iPad/iPadPro11(2021).png" },
                    new DeviceModel { Name = "iPad Pro 12.9 (2021)", Category = iPad, ImagePath = @"/images/iPad/iPadPro12.9(2021).png" },
                    new DeviceModel { Name = "iPad Air 10.9 (2020)", Category = iPad, ImagePath = @"/images/iPad/iPadAir10.9(2020).png" });

                context.DeviceModels.AddRange(
                    new DeviceModel { Name = "iMac", Category = iMac, ImagePath = @"/images/iMac/iMac.png" });

                context.DeviceModels.AddRange(
                    new DeviceModel { Name = "MacBook Air", Category = macbook, ImagePath = @"/images/Macbook/MacBookAir.png" },
                    new DeviceModel { Name = "MacBook Pro", Category = macbook, ImagePath = @"/images/Macbook/MacBookPro.png" });

                context.DeviceModels.AddRange(
                    new DeviceModel { Name = "Series 8", Category = appleWatch, ImagePath = @"/images/AppleWatch/Series8.png" },
                    new DeviceModel { Name = "Series Ultra", Category = appleWatch, ImagePath = @"/images/AppleWatch/SeriesUltra.png" },
                    new DeviceModel { Name = "Series SE 2", Category = appleWatch, ImagePath = @"/images/AppleWatch/SeriesSE2.png" },
                    new DeviceModel { Name = "Series SE", Category = appleWatch, ImagePath = @"/images/AppleWatch/SeriesSE.png" });

                context.DeviceModels.AddRange(
                    new DeviceModel { Name = "OnePlus", Category = android, ImagePath = @"/images/Android/OnePlus.png" },
                    new DeviceModel { Name = "Realme", Category = android, ImagePath = @"/images/Android/Realme.png" },
                    new DeviceModel { Name = "Samsung", Category = android, ImagePath = @"/images/Android/Samsung.png" },
                    new DeviceModel { Name = "Xiaomi", Category = android, ImagePath = @"/images/Android/Xiaomi.png" });

                context.DeviceModels.AddRange(
                    new DeviceModel { Name = "Зарядные устройства", Category = accessories, ImagePath = @"/images/Acsessories/Chargers.png" },
                    new DeviceModel { Name = "Чехлы iPhone", Category = accessories, ImagePath = @"/images/Acsessories/CasesIphone.png" },
                    new DeviceModel { Name = "Аксессуары для Apple Watch", Category = accessories, ImagePath = @"/images/Acsessories/AcsessoriesAppleWatch.png" },
                    new DeviceModel { Name = "Внешние аккумуляторы", Category = accessories, ImagePath = @"/images/Acsessories/PowerBanks.png" },
                    new DeviceModel { Name = "Dyson", Category = accessories, ImagePath = @"/images/Acsessories/Dyson.png" },
                    new DeviceModel { Name = "Защитные стекла", Category = accessories, ImagePath = @"/images/Acsessories/SafetyGlasses.png" });

                context.SaveChanges();
            }
        }
        private static void AddTestProducts(ChinilkaDbContext context)
        {
            if (!context.Products.Any())
            {
                Random rnd = new();

                var deviceModels = context.DeviceModels.ToList();

                foreach (var model in deviceModels)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        context.Products.Add(
                            new Product { Name = $"{model.Name}_testProduct_{i}", DeviceModel = model, Price = rnd.Next(20000, 100000) }
                            );
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
