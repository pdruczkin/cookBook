using cookBook.Models;
using cookBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace cookBook.Controllers
{
    [Route("api/cookBook/{recipeId}/ingredient")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public ActionResult AddIngredient([FromRoute] int recipeId, [FromBody] IngredientDto dto)
        {
            var newIngredientId = _service.Create(recipeId, dto);

            return Created($"api/cookBook/{recipeId}/ingredient/{newIngredientId}", null);

        }
    }
}