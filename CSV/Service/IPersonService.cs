using System;
using CSV.Model;

namespace CSV.Service
{
    // Service for managing persons
    public interface IPersonService
    {
        IEnumerable<Person> GetAllPersons();
        Person GetPersonById(int id);
        IEnumerable<Person> GetPersonsByColor(string color);
        void AddPerson(Person person);
    }

}

