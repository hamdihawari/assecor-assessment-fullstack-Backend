using System;
using CSV.Model;
using CSV.Service;
using Microsoft.AspNetCore.Mvc;

namespace CSV.Controllers
{
    // Controller for the REST endpoints
    [ApiController]
    [Route("api")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("persons")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            var persons = _personService.GetAllPersons();
            return Ok(persons);
        }

        [HttpGet("persons/{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet("persons/color/{color}")]
        public ActionResult<IEnumerable<Person>> GetPersonsByColor(string color)
        {
            var persons = _personService.GetPersonsByColor(color);
            return Ok(persons);
        }

        [HttpPost("persons")]
        public ActionResult AddPerson([FromBody] Person person)
        {
            _personService.AddPerson(person);
            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
        }

    }
}

