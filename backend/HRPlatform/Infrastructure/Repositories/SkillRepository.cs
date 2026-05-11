using HRPlatform.Core.Domain;
using HRPlatform.Core.RepositoryInterfaces;

namespace HRPlatform.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly PlatformDbContext _context;

        public SkillRepository(PlatformDbContext context)
        {
            _context = context;
        }

        public List<Skills> GetAll()
        {
            return _context.Skills.ToList();
        }

        public Skills GetByName(string name)
        {
            return _context.Skills
                .FirstOrDefault(s => s.Name == name);
        }

        public Skills Create(Skills skills)
        {
            _context.Skills.Add(skills);
            _context.SaveChanges();
            return skills;
        }

        public void Delete(Skills skills)
        {
            _context.Skills.Remove(skills);
            _context.SaveChanges();
        }
    }
}
