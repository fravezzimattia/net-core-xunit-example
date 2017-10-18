using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using xUnitTestDemo.Models;

namespace xUnitTestDemo.Controllers
{
    [Route("api/[controller]")]
    public class CatController : Controller
    {
        private List<Cat> Cats = new List<Cat>
        {
            new Cat
            {
                Id = 1,
                Color = "red",
                Name = "Mariella"
            },
            new Cat
            {
                Id = 2,
                Color = "black",
                Name = "Silvio"
            }
        };

        // GET api/cat
        [HttpGet]
        public IEnumerable<Cat> Get()
        {
            return Cats;
        }

        // GET api/cat/5
        [HttpGet("{id}")]
        public Cat Get(int id)
        {
            return Cats.Where(c => c.Id == id).SingleOrDefault();
        }

        // POST api/cat
        [HttpPost]
        public IActionResult Post([FromBody]Cat value)
        {
            value.Id = Cats.Max(x => x.Id) + 1;
            Cats.Add(value);
            return Created(string.Empty, new Cat { Id = value.Id });
        }
    }
}
