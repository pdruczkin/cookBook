using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cookBook.Entities;
using cookBook.Models;
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
        public ActionResult<IEnumerable<RecipeDto>> GetAll()
        {
            var recipes = _service.GetAll();
            
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDto> Get([FromRoute] int id)
        {
            var recipe = _service.Get(id);

            if (recipe is null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }


        [HttpPost]
        public ActionResult CreateRecipe([FromBody] CreateRecipeDto dto)
        {
            var id = _service.CreateRecipe(dto);

            return Created($"api/cookBook/{id}",null);

        }



    }
}
