using InfrastructureSQL.Entities;
using InfrastructureSQL.Services;
using InfrastructureSQL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(JsonConvert.SerializeObject(_userService.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_userService.GetByLastId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserEntity user)
        {
            var userResponse = await _userService.CreateUser(user);
            return Ok(JsonConvert.SerializeObject(userResponse));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserEntity user)
        {
            var userResponse = await _userService.UpdateUser(id, user.Email, user.Status, user.LastUpdatePwd);
            return Ok(JsonConvert.SerializeObject(userResponse));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userResponse = await _userService.Delete(id);
            return Ok(JsonConvert.SerializeObject(userResponse));
        }
    }
}
