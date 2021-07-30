using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cookBook.Entities;
using cookBook.Services;

namespace cookBook.Controllers
{
    [Route("api/cookBook")]
    public class CookBookController : ControllerBase
    {
        private readonly ICookBookService _service;

        public CookBookController(ICookBookService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAll()
        {
            var recipes = _service.GetAll();
            
            return Ok(recipes);
        }




    }
}
