using HRPlatform.Core.Domain;

namespace HRPlatform.Infrastructure
{
    public class DbSeeder
    {
        private readonly PlatformDbContext _context;

        public DbSeeder(PlatformDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Skills.Any())
            {
                _context.Skills.AddRange(
                        new Skills("C# programming"),
                        new Skills("C++ programming"),
                        new Skills("Java programming"),
                        new Skills("Python programming"),
                        new Skills("Angular programming"),
                        new Skills("React programming"),
                        new Skills("English language"), 
                        new Skills("German language")
                );
                _context.SaveChanges();
            }

            if (!_context.Candidates.Any())
            {
                var tea = new Candidate(
                    "Tea Pavicevic",
                    new DateTime(2003, 12, 5, 0, 0, 0, DateTimeKind.Utc),
                    "0611111111",
                    "pavicevictea6@gmail.com"
                );
                var petar = new Candidate(
                    "Petar Petrovic",
                    new DateTime(1997, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                    "0622222222",
                    "petarpetrovic@gmail.com"
                );
                var anastasija = new Candidate(
                    "Anastasija Anic",
                    new DateTime(1987, 9, 25, 0, 0, 0, DateTimeKind.Utc),
                    "0633333333",
                    "anastasijaanic@gmail.com"
                );

                _context.Candidates.AddRange(tea,petar,anastasija);
                _context.SaveChanges();
            }
        }
    }
}
