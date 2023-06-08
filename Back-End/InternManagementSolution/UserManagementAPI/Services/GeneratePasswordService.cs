using InterUserManagementAPI.Models;
using System.Diagnostics;
using UserManagementAPI.Interfaces;

namespace InterUserManagementAPI.Services
{
    public class GeneratePasswordService : IGeneratePassword
    {
        public async Task<string?> GeneratePassword(Intern intern)
        {
            string password = String.Empty;
            password = intern.Name.Substring(0, 4);
            password += intern.DateOfBirth.Day;
            password += intern.DateOfBirth.Month;
            return password;
        }
    }
}