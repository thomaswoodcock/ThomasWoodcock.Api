using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace ThomasWoodcock.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(this.Ok("Hello, World!"));
        }
    }
}