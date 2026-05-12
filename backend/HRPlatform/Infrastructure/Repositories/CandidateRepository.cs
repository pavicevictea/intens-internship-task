using HRPlatform.Core.Domain;
using HRPlatform.Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly PlatformDbContext _context;

        public CandidateRepository(PlatformDbContext context)
        {
            _context = context;
        }

        public List<Candidate> GetAll()
        {
            return _context.Candidates
                .Include(c => c.Skills)
                .ToList();
        }

        public Candidate GetById(int id)
        {
            return _context.Candidates
                .Include(c => c.Skills)
                .FirstOrDefault(c => c.CandidateId == id);
        }

        public Candidate Create(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            _context.SaveChanges();
            return candidate;
        }

        public Candidate Update(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            _context.SaveChanges();
            return candidate;
        }

        public void Delete(Candidate candidate)
        {
            if (candidate == null) return;
            _context.Candidates.Remove(candidate);
            _context.SaveChanges();
        }

        public List<Candidate> SearchCandidates(string? name, List<string> skills)
        {
            var query = _context.Candidates.Include(c => c.Skills).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.FullName.ToLower().Contains(name.ToLower()));
            }

            if (skills.Count > 0)
            {
                query = query.Where(c => c.Skills.Any(s => skills.Contains(s.Name)));
            }

            return query.ToList();
        }
    }
}
