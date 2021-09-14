using System.Collections.Generic;
using cookBook.Models;
using cookBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cookBook.Controllers
{
    [Route("api/cookBook/{recipeId}/ingredient")]
    [Authorize]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult AddIngredient([FromRoute] int recipeId, [FromBody] IngredientDto dto)
        {
            var newIngredientId = _service.Create(recipeId, dto);

            return Created($"api/cookBook/{recipeId}/ingredient/{newIngredientId}", null);
        }

        [HttpGet("{ingredientId}")]
        public ActionResult<ShowIngredientDto> GetById([FromRoute] int recipeId, [FromRoute] int ingredientId)
        {
            var ingredient = _service.GetById(recipeId, ingredientId);

            return Ok(ingredient);
        }

        [HttpGet]
        public ActionResult<List<ShowIngredientDto>> Get([FromRoute] int recipeId)
        {
            var listOfIngredients = _service.GetAll(recipeId);

            return Ok(listOfIngredients);

        }

        [HttpDelete("{ingredientId}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteById([FromRoute] int recipeId, [FromRoute] int ingredientId)
        {
            _service.Delete(recipeId,ingredientId);

            return NoContent();
        }

    }


}