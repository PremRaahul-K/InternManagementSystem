using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.DTOs
{
    public class UserApproval
    {
        public int UserId { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
    }
}
