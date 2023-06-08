using InterUserManagementAPI.Models;
using InterUserManagementAPI.Models.DTOs;
using UserManagementAPI.Models.DTOs;

namespace UserManagementAPI.Interfaces
{
    public interface IManageUser
    {
        public Task<USerDTO> Login(USerDTO user);
        public Task<USerDTO> Register(Intern intern);
        public Task<UserApproval> ChangeStatus(UserApproval userApproval);
        public Task<bool> ChangePassword(ManagePassword managePassword);

    }
}
