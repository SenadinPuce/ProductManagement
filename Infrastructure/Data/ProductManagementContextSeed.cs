using Domain.Entities;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure.Data
{
	public class ProductManagementContextSeed
	{
		public static async Task SeedAsync(ProductManagementContext context)
		{
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			if (!context.Products.Any())

			{
				var productsData = await File.ReadAllTextAsync(path + @"/Data/SeedData/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products == null)
				{
					Console.WriteLine("No products found in seed data.");
					return;
				}

				context.Products.AddRange(products);

				await context.SaveChangesAsync();
			}
		}
	}
}