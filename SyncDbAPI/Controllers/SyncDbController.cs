using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SyncDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncDbController : ControllerBase
    {
        private bool _flag = true;

        [HttpPost]
        public async Task<IActionResult> SyncData(object data)
        {
            var qwqw = JsonSerializer.Serialize(data);

            if (_flag)
            {

            }
            else
            {

            }
            return Ok(qwqw);
        }

    }
}
