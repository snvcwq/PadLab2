using Common;
using Microsoft.AspNetCore.Mvc;
using SyncNode.Services;

namespace SyncNode.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly SyncWorckJobService _workJobService;

        public SyncController(SyncWorckJobService workJobService)
        {
            _workJobService = workJobService;
        }

        [HttpPost]
        public IActionResult Sync(SyncEntity entity)
        {
            _workJobService.AddItem(entity);

            return Ok();
        }
    }
}
