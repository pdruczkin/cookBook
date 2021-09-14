using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using cookBook.Entities;
using cookBook.Models;
using cookBook.Services;
using Microsoft.AspNetCore.Authorization;

namespace cookBook.Controllers
{
    [Route("api/cookBook/recipe")]
    [Authorize]
    [ApiController]
    public class CookBookController : ControllerBase
    {
        private readonly ICookBookService _service;

        public CookBookController(ICookBookService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> GetAll([FromQuery] RecipeQuery query)
        {
            var result = _service.GetAll(query);
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDto> Get([FromRoute] int id)
        {
            var recipe = _service.Get(id);

            return Ok(recipe);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        
        public ActionResult CreateRecipe([FromBody] CreateRecipeDto dto)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } not needed because of [ApiController]*/

            //var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

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
