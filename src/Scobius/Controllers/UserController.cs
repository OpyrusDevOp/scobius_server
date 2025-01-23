using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scobius.DTOs;
using Scobius.Services;

namespace Scobius.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserService _userService) : ControllerBase
{

    [HttpPost("[action]")]
    public async Task<ActionResult<UserDto>> Registration([FromBody] string uid)
    {
        try
        {
            if ((await _userService.Exists(uid))) return Ok();
            var user = await _userService.CompleteRegistration(uid);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

}
