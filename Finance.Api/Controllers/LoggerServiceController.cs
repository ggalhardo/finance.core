using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Finance.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoggerServiceController : ControllerBase
    {

        public LoggerServiceController() { }

        [HttpPost]
        [Route("create/")]
        public async Task<IActionResult> create()
        {
            return Ok();
        }
    }
}
