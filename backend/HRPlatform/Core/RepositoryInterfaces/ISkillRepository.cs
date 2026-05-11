using HRPlatform.Core.Domain;

namespace HRPlatform.Core.RepositoryInterfaces
{
    public interface ISkillRepository
    {
        List<Skills> GetAll();
        Skills GetByName(string name);
        Skills Create(Skills skills);
        void Delete(Skills skills);
    }
}
