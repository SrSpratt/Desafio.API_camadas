using Desafio.Consumer.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Consumer.Controllers
{
    public class PersonController : Controller
    {
        private static List<PersonDto> _people;

        public PersonController()
        {
            if(_people == null)
            {
                LoadPeople();
            }
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListOfPeople()
        {
            return View(_people);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Email")] PersonDto person)
        {
            try
            {

                person.Id = Guid.NewGuid().ToString();
                _people.Add(person);
                return RedirectToAction("ListOfPeople");
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Delete(string id)
        {
            PersonDto person = _people.FirstOrDefault(x => x.Id == id);
            return View(person);
        }

        [HttpPost]
        public IActionResult DeleteHandler(string id)
        {
            var person = _people.FirstOrDefault(index => index.Id == id);
            if (person != null)
                _people.Remove(person);
            return RedirectToAction("ListOfPeople");
        }

        public IActionResult Edit(string id)
        {
            PersonDto person = _people.FirstOrDefault(x => x.Id == id);
            return View(person);
        }

        [HttpPost]
        public IActionResult EditHandler([Bind("Id, Name, Email")] PersonDto person)
        {
            var personSelected = _people.FirstOrDefault(index => index.Id == person.Id);
            if (personSelected != null)
            {
                _people.Remove(personSelected);
                _people.Add(person);
            }
            return RedirectToAction("ListOfPeople");
        }

        public IActionResult Details(string id)
        {
            PersonDto person = _people.FirstOrDefault(x => x.Id == id);
            return View(person);
        }


        private void LoadPeople()
        {
            _people = new List<PersonDto>();
            _people.Add(new PersonDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Random user",
                Email = "random@random.com"
            });
            _people.Add(new PersonDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Other random user",
                Email = "other_random@random.com"
            });
            _people.Add(new PersonDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Yet another random user",
                Email = "yet_another_random@random.com"
            });
        }
    }
}
