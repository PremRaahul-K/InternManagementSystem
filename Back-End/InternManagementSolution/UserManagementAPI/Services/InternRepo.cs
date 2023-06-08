using InterUserManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class InternRepo : IRepo<int, Intern>
    {
        private readonly UserContext _context;
        private readonly ILogger<UserRepo> _logger;

        public InternRepo(UserContext context, ILogger<UserRepo> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Intern?> Add(Intern item)
        {
            try
            {
                _context.Interns.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Intern?> Delete(int key)
        {
            try
            {
                var intern = await Get(key);
                if (intern != null)
                {
                    _context.Interns.Remove(intern);
                    await _context.SaveChangesAsync();
                    return intern;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Intern?> Get(int key)
        {
            try
            {
                var intern = await _context.Interns.Include(i=>i.User).FirstOrDefaultAsync(i=>i.Id == key);
                return intern;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Intern>?> GetAll()
        {
            try
            {
                var interns = await _context.Interns.Include(i=>i.User).ToListAsync();
                if (interns.Count > 0)
                    return interns;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Intern?> Update(Intern item)
        {
            try
            {
                var intern = await Get(item.Id);
                if (intern != null)
                {
                    intern.Id = item.Id;
                    intern.Name = item.Name;
                    intern.User = item.User;
                    intern.Email = item.Email;
                    intern.Phone = item.Phone;
                    intern.DateOfBirth = item.DateOfBirth;
                    intern.Age = item.Age;
                    intern.Gender = item.Gender;
                    return intern;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
