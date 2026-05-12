using HRPlatform.API.DTOs;
using HRPlatform.Core.Domain;
using HRPlatform.Core.RepositoryInterfaces;
using HRPlatform.Core.Services.Interfaces;

namespace HRPlatform.Core.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public List<SkillsDto> GetAll()
        {
            var skills = _skillRepository.GetAll();
            return skills.Select(s => new SkillsDto { Name = s.Name }).ToList();
        }

        public SkillsDto Create(SkillsDto dto)
        {
            var trimmedName = dto.Name.Trim();
            var existing = _skillRepository.GetAll()
                .FirstOrDefault(s => s.Name.Equals(trimmedName, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                throw new Exception("Skill with this name already exists.");
            }

            var skill = new Skills(trimmedName);
            var created = _skillRepository.Create(skill);
            return new SkillsDto { Name = created.Name };
        }

        public void Delete(string name)
        {
            var skill = _skillRepository.GetByName(name);
            _skillRepository.Delete(skill);
        }
    }
}
