using Chinilka.Interfaces;
using Chinilka.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace Chinilka.Services
{
    public class LoadCsvDataService : ILoadDataService
    {
        private readonly IChinilkaRepository repository;

        public LoadCsvDataService(IChinilkaRepository repository)
        {
            this.repository = repository;
        }
        public async Task LoadFromFileAsync(string path)
        {
            try
            {
                var products = await ParseFileToList(path);
                await repository.CreateProductsRange(products);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<Product>> ParseFileToList(string path)
        {
            List<Product> result = new();

            using (TextFieldParser parser = new TextFieldParser(path))
            {

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                var deviceModelList = await repository.DeviceModels.ToListAsync();

                while (!parser.EndOfData)
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    string[] fields = parser.ReadFields();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    var product = new Product
                    {
                        Name = fields![0],
                        Description = fields[1],
                        Price = decimal.Parse(fields[2]),
                        DeviceModel = deviceModelList.First(d => d.Name == fields[3]),
                        ImagePath = fields[4]
                    };

                    result.Add(product);
                }
            }

            return result;
        }


        private async Task CreateTestCsvData(string filePath)
        {
            var sb = new StringBuilder();
            var rnd = new Random();
            var deviceModelList = await repository.DeviceModels.Select(d => d.Name).ToListAsync();

            for (int i = 1; i <= 100; i++)
            {
                var name = $"TestProductFromCsv_{i}";
                var description = $"Описание_{i}";
                var price = rnd.Next(10000, 100000);
                var deviceModelName = deviceModelList[rnd.Next(deviceModelList.Count)];
                var imagePath = $"ImagePath_{i}";

                sb.AppendLine($"{name},{description},{price},{deviceModelName},{imagePath}");
            }
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
