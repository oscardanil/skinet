using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;
namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
       public static  async Task SeedFactory(StoreContext context, ILoggerFactory loggerfactory)
       {
           try
           {
               if(!context.ProductBrand.Any())
               {
                   var BrandData=File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                   var brands=JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                   foreach(var item in brands)
                   {
                       context.ProductBrand.Add(item);
                   }
                   await context.SaveChangesAsync();
               }
               if(!context.ProductType.Any())
               {
                   var TPData=File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                   var TP=JsonSerializer.Deserialize<List<ProductType>>(TPData);
                   foreach(var item in TP)
                   {
                       context.ProductType.Add(item);
                   }
                     await context.SaveChangesAsync();
               }

            if(!context.Products.Any())
               {
                   var productBrand=File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                   var products=JsonSerializer.Deserialize<List<Product>>(productBrand);
                   foreach(var item in products)
                   {
                       context.Products.Add(item);
                   }
                   await context.SaveChangesAsync();
               }

           }
           catch(Exception ex)
           {
                var logger = loggerfactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
           }
       } 
    }
}