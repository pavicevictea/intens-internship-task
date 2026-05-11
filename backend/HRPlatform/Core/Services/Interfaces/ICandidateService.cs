using HRPlatform.API.DTOs;

namespace HRPlatform.Core.Services.Interfaces
{
    public interface ICandidateService
    {
        List<CandidateDto> GetAll();
        CandidateDto GetById(int id);
        CandidateDto Create(CreateCandidateDto dto);
        CandidateDto Update(int id, CreateCandidateDto dto);
        void Delete(int id);
        List<CandidateDto> Search(string? name, List<string> skills);
        void AddSkillToCandidate(int id, string skillName);
        void RemoveSkillFromCandidate(int id, string skillName);
    }
}
