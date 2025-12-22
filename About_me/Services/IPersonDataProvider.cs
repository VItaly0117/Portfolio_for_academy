using About_me.Models;

namespace About_me.Services
{
    public interface IPersonDataProvider
    {
        // Метод для получения человека по ID
        Person GetPersonById(int id);
    }
}