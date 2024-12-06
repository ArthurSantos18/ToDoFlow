using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRepository : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(UserCreateDto userCreateDto)
        {
            return Ok(await _userRepository.CreateUserAsync(userCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult> ReadUserAsync()
        {
            return Ok(await _userRepository.ReadUserAsync());
        }

        [HttpGet("id")]
        public async Task<ActionResult> ReadUserAsync(int id)
        {
            return Ok(await _userRepository.ReadUserAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            return Ok(await _userRepository.UpdateUserAsync(userUpdateDto));
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            return Ok(await _userRepository.DeleteUserAsync(id));
        }
    }
}
