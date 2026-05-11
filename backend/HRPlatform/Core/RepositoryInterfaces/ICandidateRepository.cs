using HRPlatform.Core.Domain;

namespace HRPlatform.Core.RepositoryInterfaces
{
    public interface ICandidateRepository
    {
        List<Candidate> GetAll();
        Candidate GetById(int id);
        Candidate Create(Candidate candidate);
        Candidate Update(Candidate candidate);
        void Delete(Candidate candidate);
        List<Candidate> SearchCandidates(string? name, List<string> skills);

    }
}
