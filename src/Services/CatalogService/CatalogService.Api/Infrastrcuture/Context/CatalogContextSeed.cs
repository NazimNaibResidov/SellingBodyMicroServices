using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace CatalogService.Api.Infrastrcuture.Context
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext context, IWebHostEnvironment env, ILogger<CatalogContextSeed> seed)
        {
        }

        private async void GetCatalogPrictures(string contentPath, string picturePath)
        {
            picturePath ??= "pics";
            if (picturePath != null)
            {
                DirectoryInfo info = new DirectoryInfo(picturePath);
                foreach (var item in info.GetFiles())
                {
                    item.Delete();
                }
                string zipFileCatalogItemsPictur = Path.Combine(contentPath, "catalogItem.zip");
                ZipFile.ExtractToDirectory(zipFileCatalogItemsPictur, picturePath);
            }
        }
    }
}