using System.Collections.Generic;
using System.Linq;
using cookBook.Models;
using cookBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cookBook.Controllers
{
    [Route("api/cookBook/{recipeId}/steps")]
    [Authorize]
    [ApiController]
    public class StepsController : ControllerBase
    {
        private readonly IStepsService _stepsService;

        public StepsController(IStepsService stepsService)
        {
            _stepsService = stepsService;
        }


        [HttpGet]
        public ActionResult<List<string>> GetAllSteps([FromRoute] int recipeId)
        {
            var stepsDto = _stepsService.GetAllSteps(recipeId);

            return Ok(stepsDto);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult ModifySteps([FromRoute] int recipeId,[FromBody] List<string> newSteps)
        {

            if (newSteps == null || newSteps.Any(s => s.Length > 250))
            {
                ModelState.AddModelError("Steps","Steps can't be null and any step can't have more than 250 letters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _stepsService.Update(recipeId, newSteps);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete([FromRoute] int recipeId)
        {

            _stepsService.Delete(recipeId);

            return NoContent();

        }

    }
}