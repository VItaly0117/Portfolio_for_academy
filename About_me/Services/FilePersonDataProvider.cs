using System.Text.Json;
using About_me.Models;

namespace About_me.Services
{
    public class FilePersonDataProvider : IPersonDataProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FilePersonDataProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public Person GetPersonById(int id)
        {
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "persondata.json");

            if (!File.Exists(filePath))
            {
                return null;
            }
            string jsonString = File.ReadAllText(filePath);
            var people = JsonSerializer.Deserialize<List<Person>>(jsonString);
            return people?.FirstOrDefault(p => p.Id == id);
        }
    }
}