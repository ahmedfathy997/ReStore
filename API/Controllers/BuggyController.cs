using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized();
        }
        [HttpGet("validation-error")]
        public IActionResult GetNotValidationError()
        {
            ModelState.AddModelError("Problem_1:", "This is the first error");
            ModelState.AddModelError("Problem_1:", "This is the first error");
            return ValidationProblem();
        }
        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }

    }
}
