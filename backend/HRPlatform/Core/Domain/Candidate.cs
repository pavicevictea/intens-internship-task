namespace HRPlatform.Core.Domain
{
    public class Candidate
    {
        public int CandidateId { get; private set; }
        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string ContactNumber { get; private set; }
        public string Email { get; private set; }

        public ICollection<Skills> Skills { get; private set; } = new List<Skills>();

        private Candidate() { }

        public Candidate(string fullName, DateTime dateOfBirth, string contactNumber, string email)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber;
            Email = email;
        }

    }
}
