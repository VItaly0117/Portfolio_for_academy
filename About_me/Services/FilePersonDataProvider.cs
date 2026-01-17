using System.Text.Json;
using About_me.Models;

namespace About_me.Services
{
    public class FilePersonDataProvider : IPersonDataProvider
    {
        // Поле для хранения информации об окружении (пути к файлам)
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Конструктор
        public FilePersonDataProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // Метод 1: Получить данные (мы его писали раньше)
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

        // Метод 2: Обновить данные (тот, что с ошибкой)
        public void UpdatePerson(Person person)
        {
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "persondata.json");

            if (!File.Exists(filePath)) return;

            // 1. Читаем весь список
            string jsonString = File.ReadAllText(filePath);
            var people = JsonSerializer.Deserialize<List<Person>>(jsonString) ?? new List<Person>();

            // 2. Знаходимо користувача, якого треба оновити
            var existingPerson = people.FirstOrDefault(p => p.Id == person.Id);

            if (existingPerson != null)
            {
                // 3. Оновлюємо поля
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.JobTitle = person.JobTitle;
                existingPerson.Description = person.Description;
                existingPerson.Email = person.Email;
                existingPerson.Phone = person.Phone;
                existingPerson.Skills = person.Skills;

                // 4. Зберігаємо назад у файл
                var newJson = JsonSerializer.Serialize(people, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, newJson);
            }
        }
    }
}