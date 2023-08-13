using System;
using CSV.Model;
using System.Collections.Generic;

namespace CSV.Service
{
    public class CsvPersonService : IPersonService
    {
        private readonly string _csvFilePath;
        private readonly List<Person> _persons;

        public CsvPersonService(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
            _persons = LoadDataFromCsv();
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _persons;
        }

        
        public Person GetPersonById(int id)
        {
            var person = _persons.SingleOrDefault(person => person.Id == id);
            return person; // If person is null, this will return null
        }


        public IEnumerable<Person> GetPersonsByColor(string color)
        {
            return _persons.Where(person => person.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        }

        public void AddPerson(Person person)
        {
            if (_persons.Count == 0)
            {
                person.Id = 1; 
            }
            else
            {
                int newId = _persons.Max(p => p.Id) + 1; 
                person.Id = newId; 
            }

            _persons.Add(person);
            SaveDataToCsv();
        }

        private List<Person> LoadDataFromCsv()
        {
            var persons = new List<Person>();
            using (var reader = new StreamReader(_csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length >= 7)
                    {
                        var person = new Person
                        {
                            Id = int.Parse(values[0]),
                            Name = values[1],
                            LastName = values[2],
                            ZipCode = values[3],
                            City = values[4],
                            Color = values[5]
                        };
                        persons.Add(person);
                    }
                }
            }
            return persons;
        }

        private void SaveDataToCsv()
        {
            using (var writer = new StreamWriter(_csvFilePath))
            {
                foreach (var person in _persons)
                {
                    writer.WriteLine($"{person.Id},{person.Name},{person.LastName},{person.ZipCode},{person.City},{person.Color}");
                }
            }
        }
    }
}

