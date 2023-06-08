using UserManagementAPI.Models.DTOs;

namespace UserManagementAPI.Interfaces
{
    public interface IRetriveUsers<K>
    {
        Task<ICollection<UserApproval>> GetAllUsers();  
        Task<UserApproval> GetUserApproval(K id);
        Task<ICollection<UserApproval>> GetNonApprovedUsers();
        Task<ICollection<UserApproval>> GetApprovedUsers();

    }
}
