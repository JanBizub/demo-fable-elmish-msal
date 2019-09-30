using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AADDemo.Controllers
{
  [Authorize]
  [EnableCors("MyPolicy")]
  [ApiController]
  [Route("[controller]")]
  public class CarController : ControllerBase
  {
    [HttpGet] public string Get() => "Honda Legend";
  }
}