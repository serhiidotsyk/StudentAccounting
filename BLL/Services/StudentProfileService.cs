using AutoMapper;
using BLL.Interfaces;
using BLL.Models.StudentProfile;
using DAL;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StudentProfileService : IStudentProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentProfileService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserModel> GetStudentProfile(int id)
        {
            var student = await _context.Users.FindAsync(id);
            if (student != null)
            {
                return _mapper.Map<UserModel>(student);
            }                    

            return null;           
        }
    }
}
