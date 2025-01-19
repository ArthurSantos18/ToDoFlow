using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        //[Authorize (Roles = "Adm")]
        public async Task<ActionResult> CreateUserAsync(UserCreateDto userCreateDto)
        {
            return Ok(await _userService.CreateUserAsync(userCreateDto));
        }

        [HttpGet]
        //[Authorize(Roles = "Adm")]
        public async Task<ActionResult> ReadUserAsync()
        {
            return Ok(await _userService.ReadUserAsync());
        }

        [HttpGet("id")]
       //[Authorize(Roles = "Adm")]
        public async Task<ActionResult> ReadUserAsync(int id)
        {
            return Ok(await _userService.ReadUserAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            return Ok(await _userService.UpdateUserAsync(userUpdateDto));
        }

        [HttpDelete("id")]
        //[Authorize(Roles = "Adm")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            return Ok(await _userService.DeleteUserAsync(id));
        }
    }
}
