using Microsoft.AspNetCore.Mvc;

namespace DemoHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(":)");
        }
    }
}
