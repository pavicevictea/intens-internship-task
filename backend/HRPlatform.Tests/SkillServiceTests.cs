using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPlatform.API.DTOs;
using HRPlatform.Core.Domain;
using HRPlatform.Core.RepositoryInterfaces;
using HRPlatform.Core.Services;
using Moq;

namespace HRPlatform.Tests
{
    public class SkillServiceTests
    {
        private readonly Mock<ISkillRepository> _skillRepositoryMock;
        private readonly SkillService _service;

        public SkillServiceTests()
        {
            _skillRepositoryMock = new Mock<ISkillRepository>();
            _service = new SkillService(_skillRepositoryMock.Object);
        }

        [Fact]
        public void Create_Should_Save_Skill_Correctly()
        {
            // Arrange
            var dto = new SkillsDto() { Name = "  Python                " };
            _skillRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Skills>());
            _skillRepositoryMock.Setup(r => r.Create(It.IsAny<Skills>())).Returns((Skills s) => s);

            // Act
            var result = _service.Create(dto);

            // Assert
            Assert.Equal("Python", result.Name);
            _skillRepositoryMock.Verify(r => r.Create(It.IsAny<Skills>()), Times.Once);
        }

        [Fact]
        public void Delete_Skill_Test()
        {
            // Arrange
            var s = new Skills("Italian language");
            _skillRepositoryMock.Setup(r => r.GetByName("Italian language")).Returns(s);

            // Act
            _service.Delete("Italian language");

            // Assert
            _skillRepositoryMock.Verify(r => r.Delete(s), Times.Once);
        }
    }
}
