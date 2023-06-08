using InterUserManagementAPI.Models;
using InterUserManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IManageUser _manageUser;

        public UserController(IManageUser manageUser)
        {
            _manageUser = manageUser;
        }
        [HttpPost]
        public async Task<ActionResult<USerDTO>> Register(Intern intern)
        {
            var result = await _manageUser.Register(intern);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to register at this moment");
        }
        [HttpPost]
        public async Task<ActionResult<USerDTO>> Login([FromBody] USerDTO userDTO)
        {
            var user = await _manageUser.Login(userDTO);
            if (user != null)
            {
                return BadRequest("invalid username or password");
            }
            return Ok(user);
        }
    }
}
