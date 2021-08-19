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
    [Route("api/cookBook/recipe")]
    [ApiController]
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

            return Ok(recipe);
        }


        [HttpPost]
        public ActionResult CreateRecipe([FromBody] CreateRecipeDto dto)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } not needed because of [ApiController]*/ 
            
            var id = _service.CreateRecipe(dto);

            return Created($"api/cookBook/{id}",null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {

            _service.Delete(id);

            return NoContent();

        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRecipeDto dto,[FromRoute] int id)
        {
            _service.Update(id, dto);

            return Ok();
        
        }
    }
}
