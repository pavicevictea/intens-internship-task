using HRPlatform.API.DTOs;
using HRPlatform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRPlatform.API.Controllers
{
    [ApiController]
    [Route("api/candidates")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public ActionResult<List<CandidateDto>> GetAll()
        {
            return Ok(_candidateService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<CandidateDto> GetById(int id)
        {
            var candidate = _candidateService.GetById(id);
            return Ok(candidate);
        }

        [HttpPost]
        public ActionResult<CandidateDto> Create([FromBody] CreateCandidateDto dto)
        {
            var created = _candidateService.Create(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public ActionResult<CandidateDto> Update(int id, [FromBody] CreateCandidateDto dto)
        {
            var updated = _candidateService.Update(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _candidateService.Delete(id);
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<List<CandidateDto>> Search([FromQuery] string? name, [FromQuery] List<string> skills)
        {
            var results = _candidateService.Search(name, skills);
            return Ok(results);
        }

        [HttpPost("{id}/skills/{skillName}")]
        public ActionResult AddSkillToCandidate(int id, string skillName)
        {
            _candidateService.AddSkillToCandidate(id,skillName);
            return Ok();
        }

        [HttpDelete("{id}/skills/{skillName}")]
        public ActionResult RemoveSkillFromCandidate(int id, string skillName)
        {
            _candidateService.RemoveSkillFromCandidate(id, skillName);
            return NoContent();
        }
    }
}
