using InterUserManagementAPI.Models;
using InterUserManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models.DTOs;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AngularCORS")]
    public class UserController : ControllerBase
    {
        private readonly IManageUser _manageUser;
        private readonly IRepo<int, Intern> _internRepo;
        private readonly IRetriveUsers<int> _userRepo;

        public UserController(IManageUser manageUser,IRepo<int,Intern> internRepo,IRetriveUsers<int> userRepo)
        {
            _manageUser = manageUser;
            _internRepo = internRepo;
            _userRepo = userRepo;
        }
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<USerDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<USerDTO>> Register(Intern intern)
        {
            var result = await _manageUser.Register(intern);
            if (result != null)
            {
                return Created("Home",result);
            }
            return BadRequest("Unable to register at this moment");
        }
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<USerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<USerDTO>> Login([FromBody] USerDTO userDTO)
        {
            var user = await _manageUser.Login(userDTO);
            if (user == null)
            {
                return BadRequest("invalid username or password");
            }
            return Ok(user);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActionResult<ICollection<Intern>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<USerDTO>> GetAllInterns()
        {
            var users = await _internRepo.GetAll();
            if (users == null)
            {
                return NotFound("No interns are available at the moment");
            }
            return Ok(users);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActionResult<ICollection<UserApproval>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<UserApproval>>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();
            if (users == null)
            {
                return NotFound("No users are available at the moment");
            }
            return Ok(users);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActionResult<ICollection<UserApproval>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<UserApproval>>> GetAllNonApprovedUsers()
        {
            var users = await _userRepo.GetNonApprovedUsers();
            if (users == null)
            {
                return NotFound("No users are available at the moment");
            }
            return Ok(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActionResult<ICollection<UserApproval>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<UserApproval>>> GetAllApprovedUsers()
        {
            var users = await _userRepo.GetApprovedUsers();
            if (users == null)
            {
                return NotFound("No users are available at the moment");
            }
            return Ok(users);
        }
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassword(ManagePassword managePassword)
        {
            var result = await _manageUser.ChangePassword(managePassword);
            if (result)
            {
                return Accepted("Password has been updated Successfully");
            }
            return BadRequest("Unable to update the password");

        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActionResult<UserApproval>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserApproval>> UpdateUserStatus(UserApproval userApproval)
        {
            var result = await _manageUser.ChangeStatus(userApproval);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to update the status");

        }
    }
}
