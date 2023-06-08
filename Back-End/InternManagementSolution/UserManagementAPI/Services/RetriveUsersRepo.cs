using InterUserManagementAPI.Models;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models.DTOs;

namespace UserManagementAPI.Services
{
    public class RetriveUsersRepo : IRetriveUsers<int>
    {
        private readonly IRepo<int, User> _userRepo;

        public RetriveUsersRepo(IRepo<int,User> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<ICollection<UserApproval>> GetAllUsers()
        {
            var result = new List<UserApproval>();
            var users = (await _userRepo.GetAll()).ToList();
            if (users.Count()>0)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    var userApproval = new UserApproval();
                    userApproval.UserId = users[i].UserId;
                    userApproval.Status = users[i].Status;
                    userApproval.UserId = users[i].UserId;
                    userApproval.Role = users[i].Role;
                    result.Add(userApproval);
                }
            }
            return result;
        }

        public async Task<ICollection<UserApproval>> GetApprovedUsers()
        {
            var result = (await GetAllUsers()).Where(u => u.Status == "Approved").ToList();
            if (result.Count() > 0)
            {
                return result;
            }
            return null;
        }

        public async Task<ICollection<UserApproval>> GetNonApprovedUsers()
        {
            var result = (await GetAllUsers()).Where(u=>u.Status == "Not Approved").ToList();
            if (result.Count()>0)
            {
                return result;
            }
            return null;
        }

        public async Task<UserApproval> GetUserApproval(int id)
        {
            var result = await _userRepo.Get(id);
            if (result!=null)
            {
                var userApproval = new UserApproval();
                userApproval.UserId = result.UserId;
                userApproval.Status = result.Status;
                userApproval.Role= result.Role;
                return userApproval;
            }
            return null;
        }
    }
}
