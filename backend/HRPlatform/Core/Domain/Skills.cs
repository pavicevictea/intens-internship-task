namespace HRPlatform.Core.Domain
{
    public class Skills
    {
        public int SkillsId { get; private set; }
        public string Name { get; private set; }

        public ICollection<Candidate> Candidates { get; private set; } = new List<Candidate>();

        private Skills() { }

        public Skills(string name)
        {
            Name = name;
        }
    }
}
