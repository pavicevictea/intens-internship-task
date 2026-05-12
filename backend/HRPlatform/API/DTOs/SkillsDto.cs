using System.ComponentModel.DataAnnotations;

namespace HRPlatform.API.DTOs
{
    public class SkillsDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
