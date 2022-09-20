using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using People.Data;
using People.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace People.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
          @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var repo = new PeopleDb(_connectionString);
            List<Person> people = repo.GetAll();
            return Json(people);
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            var repo = new PeopleDb(_connectionString);
            repo.AddPerson(person);
            return Json(person);
        }
        [HttpPost]
        public void Update(Person person)
        {
            var repo = new PeopleDb(_connectionString);
            repo.Update(person);
            
        }
        [HttpPost]
        public void Delete(int id)
        {
            var repo = new PeopleDb(_connectionString);
            repo.Delete(id);
           
        }



    }
}
