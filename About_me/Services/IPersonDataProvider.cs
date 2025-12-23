using About_me.Models;

namespace About_me.Services
{
    public interface IPersonDataProvider
    {
        Person GetPersonById(int id);
        void UpdatePerson(Person person);
    }
}