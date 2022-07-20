using Bogus;
using CatalogService.Api.Core.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Api.Infrastrcuture.Context
{
    public static class CatalogContextSeed
    {
        public const string _path = "http://fakeimg.pl/350x200/ff0000/000";
        private static void CatalogBrendSeed(CatalogContext context)
        {
            var CatalogBrend = new Faker<CatalogBrend>()
                .RuleFor(x => x.Brend, x => x.Commerce.Product())
                .Generate(100);
            context.CatalogBrends.AddRange(CatalogBrend);
            context.SaveChanges();
        }
        private static void CatalogTypeSeed(CatalogContext context)
        {
            var CatalogBrend = new Faker<CatalogType>()
                .RuleFor(x => x.Type, x => x.Commerce.Product())
                .Generate(100);
            context.CatalogTypes.AddRange(CatalogBrend);
            context.SaveChanges();
        }
        private static void CatalogItemSeed(CatalogContext context)
        {
            int CatalogBrendId = 1;
            int CatalogTypeId = 1;
            var CatalogItems = new Faker<CatalogItem>()
            .RuleFor(x => x.Name, x => x.Commerce.ProductName())
            .RuleFor(x => x.Description, x => x.Commerce.ProductDescription())
            .RuleFor(x => x.PictureFileName, _path)
            .RuleFor(x => x.PictureUrl, _path)
            .RuleFor(x=>x.CatalogBrendId,CatalogBrendId++)
            .RuleFor(x=>x.CatalogTypeId,CatalogTypeId++)
            .RuleFor(x => x.Price, x => x.Random.Number(10, 100))
            .Generate(100);
            context.CatalogItems.AddRange(CatalogItems);
            context.SaveChanges();

        }
        public static void SeedAsync(this IServiceCollection service)
        {
            var sp = service.BuildServiceProvider();
            var context= sp.GetRequiredService<CatalogContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
            if (!context.CatalogItems.Any())
            {
                CatalogBrendSeed(context);
                CatalogTypeSeed(context);
                CatalogItemSeed(context);
            }
           
            
        }

        //private async void GetCatalogPrictures(string contentPath, string picturePath)
        //{
        //    picturePath ??= "pics";
        //    if (picturePath != null)
        //    {
        //        DirectoryInfo info = new DirectoryInfo(picturePath);
        //        foreach (var item in info.GetFiles())
        //        {
        //            item.Delete();
        //        }
        //        string zipFileCatalogItemsPictur = Path.Combine(contentPath, "catalogItem.zip");
        //        ZipFile.ExtractToDirectory(zipFileCatalogItemsPictur, picturePath);
        //    }
        //}
    }
}