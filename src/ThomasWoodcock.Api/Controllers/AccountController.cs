using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace ThomasWoodcock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return await Task.FromResult(this.Ok("Hello, World!"));
        }
    }
}
