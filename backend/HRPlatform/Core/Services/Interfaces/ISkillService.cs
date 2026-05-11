using HRPlatform.API.DTOs;

namespace HRPlatform.Core.Services.Interfaces
{
    public interface ISkillService
    {
        List<SkillsDto> GetAll();
        SkillsDto Create(SkillsDto dto);
        void Delete(string name);
    }
}
