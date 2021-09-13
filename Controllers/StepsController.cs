using System.Collections.Generic;
using cookBook.Models;
using cookBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace cookBook.Controllers
{
    [Route("api/cookBook/{recipeId}/steps")]
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




    }
}