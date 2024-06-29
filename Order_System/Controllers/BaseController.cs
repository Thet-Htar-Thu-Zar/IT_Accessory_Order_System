using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Order_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Content(object obj)
        {
            return Ok(JsonConvert.SerializeObject(obj));
        }

        protected IActionResult InternalServerError(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        protected IActionResult Created(string message)
        {
            return StatusCode(201, message);
        }

        protected IActionResult Accepted(string message)
        {
            return StatusCode(202, message);
        }
    }
}
