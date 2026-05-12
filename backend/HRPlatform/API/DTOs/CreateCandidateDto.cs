using System.ComponentModel.DataAnnotations;

namespace HRPlatform.API.DTOs
{
    public class CreateCandidateDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string ContactNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
    }
}
