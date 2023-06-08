using InterUserManagementAPI.Models;
using InterUserManagementAPI.Models.DTOs;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models.DTOs;

namespace UserManagementAPI.Services
{
    public class ManageUserService : IManageUser
    {
        private readonly IRepo<int, User> _userRepo;
        private readonly IRepo<int, Intern> _internRepo;
        private readonly IGeneratePassword _passwordService;
        private readonly ITokenGenerate _tokenService;

        public ManageUserService(IRepo<int, User> userRepo,
            IRepo<int, Intern> internRepo,
            IGeneratePassword passwordService,
            ITokenGenerate tokenService)
        {
            _userRepo = userRepo;
            _internRepo = internRepo;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<bool> ChangePassword(ManagePassword managePassword)
        {
            var userData = await _userRepo.Get(managePassword.UserId);
            if (userData != null) 
            {
                var hmac = new HMACSHA512();
                userData.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(managePassword.NewPassword));
                userData.PasswordKey = hmac.Key;
                var result = await _userRepo.Update(userData);
                if (result!=null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<UserApproval> ChangeStatus(UserApproval userApproval)
        {
            var userData = await _userRepo.Get(userApproval.UserId);
            if (userData != null)
            {
                userData.Status = userApproval.Status;
                var result = await _userRepo.Update(userData);
                if (result != null)
                {
                    return userApproval;
                }
            }
            return null;
        }

        public async Task<USerDTO> Login(USerDTO user)
        {
            var userData = await _userRepo.Get(user.UserId);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.PasswordKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.PasswordHash[i])
                        return null;
                }
                user = new USerDTO();
                user.UserId = userData.UserId;
                user.Role = userData.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

        public async Task<USerDTO> Register(Intern intern)
        {

            USerDTO user = null;
            var hmac = new HMACSHA512();
            string generatedPassword = await _passwordService.GeneratePassword(intern);
            intern.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(generatedPassword));
            intern.User.PasswordKey = hmac.Key;
            intern.User.Role = "Intern";
            intern.User.Status = "Not Approved";
            var userResult = await _userRepo.Add(intern.User);
            var internResult = await _internRepo.Add(intern);
            if (userResult != null && internResult != null)
            {
                user = new USerDTO();
                user.UserId = internResult.Id;
                user.Role = userResult.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;

        }
    }
}
