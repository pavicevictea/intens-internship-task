using HRPlatform.API.DTOs;
using HRPlatform.Core.Domain;
using HRPlatform.Core.RepositoryInterfaces;
using HRPlatform.Core.Services;
using Moq;

namespace HRPlatform.Tests
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly Mock<ISkillRepository> _skillRepositoryMock;
        private readonly CandidateService _service;

        public CandidateServiceTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _skillRepositoryMock = new Mock<ISkillRepository>();
            _service = new CandidateService(_candidateRepositoryMock.Object, _skillRepositoryMock.Object);
        }

        [Fact]
        public void Create_Candidate_With_Duplicate_Skills_Should_Work()
        {
            // Arrange
            var dto = new CreateCandidateDto
            {
                FullName = "Tea Pavicevic",
                DateOfBirth = new DateTime(2002, 5, 12),
                ContactNumber = "0641234567",
                Email = "tea@test.com",
                Skills = new List<string> { "C#", "  c#  ", "JAVA", "C#" }
            };
            _skillRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Skills>());
            _candidateRepositoryMock.Setup(r => r.Create(It.IsAny<Candidate>())).Returns((Candidate c) => c);

            // Act
            var result = _service.Create(dto);

            // Assert
            Assert.Equal(2, result.Skills.Count);
            Assert.Contains("C#", result.Skills);
            Assert.Contains("JAVA", result.Skills);

        }

        [Fact]
        public void Update_Candidate_Should_Replace_Skills_And_Info_Correctly()
        {
            // Arrange
            var existing = new Candidate("Marko Markovic", new DateTime(1995, 5, 5), "0651234567", "marko@test.com");
            existing.Skills.Add(new Skills("JavaScript"));
            var updateDto = new CreateCandidateDto
            {
                FullName = "Marko Petrovic",
                DateOfBirth = new DateTime(1995, 5, 5),
                ContactNumber = "0651234567",
                Email = "marko.p@test.com",
                Skills = new List<string> { "TypeScript", "Angular" }
            };
            _candidateRepositoryMock.Setup(r => r.GetById(11)).Returns(existing);
            _skillRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Skills>());
            _candidateRepositoryMock.Setup(r => r.Update(It.IsAny<Candidate>())).Returns((Candidate c) => c);

            // Act
            var result = _service.Update(11, updateDto);

            // Assert
            Assert.Equal("Marko Petrovic", result.FullName);
            Assert.Equal(2, result.Skills.Count);
            Assert.Contains("TypeScript", result.Skills);
            Assert.DoesNotContain("JavaScript", result.Skills);
            _candidateRepositoryMock.Verify(r => r.Update(It.IsAny<Candidate>()), Times.Once);
        }

        [Fact]
        public void Delete_Candidate_Should_Call_Repository()
        {
            // Arrange
            var candidate = new Candidate("Jovana Jovic", new DateTime(1998, 10, 20), "0636663333", "jovana@test.com");
            _candidateRepositoryMock.Setup(r => r.GetById(7)).Returns(candidate);

            // Act
            _service.Delete(7);

            // Assert
            _candidateRepositoryMock.Verify(r => r.Delete(candidate), Times.Once);
        }

        [Fact]
        public void Search_Candidates_Should_Return_Correct_Results()
        {
            // Arrange
            var candidatesList = new List<Candidate>
            {
                new Candidate("Nikola Nikolic", new DateTime(1992, 12, 1), "0616161616", "nikola@test.com")
            };
            _candidateRepositoryMock.Setup(r => r.SearchCandidates(It.IsAny<string>(), It.IsAny<List<string>>()))
                .Returns(candidatesList);

            // Act
            var result = _service.Search("Nikola", new List<string>());

            // Assert
            Assert.Single(result);
            Assert.Equal("Nikola Nikolic", result[0].FullName);
        }

        [Fact]
        public void Add_Skill_To_Candidate_Should_Update_Repository()
        {
            // Arrange
            var candidate = new Candidate("Sara Saric", new DateTime(2001, 7, 7), "0693214321", "sara@test.com"); 
            _candidateRepositoryMock.Setup(r => r.GetById(123)).Returns(candidate);
            _skillRepositoryMock.Setup(r => r.GetByName("Docker")).Returns((Skills)null!);

            // Act
            _service.AddSkillToCandidate(123, "Docker");

            // Assert
            Assert.Contains(candidate.Skills, s => s.Name == "Docker");
            _candidateRepositoryMock.Verify(r => r.Update(candidate), Times.Once);
        }

        [Fact]
        public void Remove_Skill_From_Candidate_Should_Update_Repository()
        {
            // Arrange
            var candidate = new Candidate("Luka Lukic", new DateTime(1999, 2, 2), "0621231234", "luka@test.com");
            candidate.Skills.Add(new Skills("SQL"));
            _candidateRepositoryMock.Setup(r => r.GetById(15)).Returns(candidate);

            // Act
            _service.RemoveSkillFromCandidate(15, "SQL");

            // Assert
            Assert.Empty(candidate.Skills);
            _candidateRepositoryMock.Verify(r => r.Update(candidate), Times.Once);
        }
    }
}