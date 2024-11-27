using FoodDeliveryAPI.Entities;
using FoodDeliveryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRep;
        private readonly ISyncService<User> _syncService;
        public UserController(IRepository<User> userRep, ISyncService<User> syncService)
        {
            _userRep = userRep;
            _syncService = syncService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var entities = await _userRep.GetAsync();

            var result = entities.Select(entity => new
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var entity = await _userRep.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var result = new
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateUser(UserRequest model)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                LastChangeAt = DateTime.UtcNow,
            };

            await _userRep.AddAsync(user);

            _syncService.Upsert(user);

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditUser(User model)
        {
            model.LastChangeAt = DateTime.Now;
            await _userRep.UpdateAsync(model);

            _syncService.Upsert(model);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var entity = await _userRep.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }
            
            await _userRep.DeleteAsync(entity);

            entity.LastChangeAt = DateTime.Now;
            _syncService.Delete(entity);

            return Ok("Deleted " + id);
        }

        [HttpPut("sync")]
        public async Task<IActionResult> UpsertSync(User user)
        {
            var existingUser = await _userRep.GetByIdAsync(user.Id);

            if(existingUser == null)
            {
                await _userRep.AddAsync(user);
            }
            else if (user.LastChangeAt > existingUser.LastChangeAt)
            {
                await _userRep.UpdateAsync(user);
            }

            return Ok();
        }

        [HttpDelete("sync")]
        public async Task<IActionResult> DeleteSync(User user)
        {
            var existingUser = await _userRep.GetByIdAsync(user.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (existingUser != null || user.LastChangeAt > existingUser.LastChangeAt)
            {
                await _userRep.DeleteAsync(existingUser);
            }

            return Ok();
        }
    }
}
