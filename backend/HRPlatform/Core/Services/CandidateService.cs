using HRPlatform.API.DTOs;
using HRPlatform.Core.Domain;
using HRPlatform.Core.RepositoryInterfaces;
using HRPlatform.Core.Services.Interfaces;

namespace HRPlatform.Core.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;

        public CandidateService(ICandidateRepository candidateRepository, ISkillRepository skillRepository)
        {
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
        }

        public List<CandidateDto> GetAll()
        {
            var candidates = _candidateRepository.GetAll();
            return candidates.Select(c => MapToDto(c)).ToList();
        }

        public CandidateDto GetById(int id)
        {
            var candidate = _candidateRepository.GetById(id);
            return MapToDto(candidate);
        }

        public CandidateDto Create(CreateCandidateDto dto)
        {
            var candidate = new Candidate(dto.FullName, dto.DateOfBirth, dto.ContactNumber, dto.Email);

            foreach (var skillName in dto.Skills)
            {
                var skill = _skillRepository.GetByName(skillName) ?? new Skills(skillName);
                candidate.Skills.Add(skill);
            }

            var created = _candidateRepository.Create(candidate);
            return MapToDto(candidate);
        }

        public CandidateDto Update(int id, CreateCandidateDto dto)
        {
            var candidate = _candidateRepository.GetById(id);
            candidate.UpdateInfo(dto.FullName, dto.DateOfBirth, dto.ContactNumber, dto.Email);

            candidate.Skills.Clear();
            foreach (var skillName in dto.Skills)
            {
                var skill = _skillRepository.GetByName(skillName) ?? new Skills(skillName);
                candidate.Skills.Add(skill);
            }

            var updatedCandidate = _candidateRepository.Update(candidate);
            return MapToDto(updatedCandidate);
        }

        public void Delete(int id)
        {
            var candidate = _candidateRepository.GetById(id);
            _candidateRepository.Delete(candidate);
        }

        public List<CandidateDto> Search(string? name, List<string> skills)
        {
            var results = _candidateRepository.SearchCandidates(name, skills);
            return results.Select(c => MapToDto(c)).ToList();
        }

        public void AddSkillToCandidate(int id, string skillName)
        {
            var candidate = _candidateRepository.GetById(id);
            var skill = _skillRepository.GetByName(skillName) ?? new Skills(skillName);
            candidate.Skills.Add(skill);
            _candidateRepository.Update(candidate);
        }

        public void RemoveSkillFromCandidate(int id, string skillName)
        {
            var candidate = _candidateRepository.GetById(id);
            var skill = candidate.Skills.FirstOrDefault(s => s.Name == skillName);
            candidate.Skills.Remove(skill);
            _candidateRepository.Update(candidate);
        }

        private CandidateDto MapToDto(Candidate c)
        {
            return new CandidateDto
            {
                CandidateId = c.CandidateId,
                FullName = c.FullName,
                DateOfBirth = c.DateOfBirth,
                ContactNumber = c.ContactNumber,
                Email = c.Email,
                Skills = c.Skills.Select(s => s.Name).ToList()
            };
        }

    }
}
