using HRPlatform.API.DTOs;
using HRPlatform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRPlatform.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public ActionResult<List<SkillsDto>> GetAll()
        {
            return Ok(_skillService.GetAll());
        }

        [HttpPost]
        public ActionResult<SkillsDto> Create([FromBody] SkillsDto dto)
        {
            var created = _skillService.Create(dto);
            return Ok(created);
        }

        [HttpDelete("{name}")]
        public ActionResult Delete(string name)
        {
            _skillService.Delete(name);
            return NoContent();
        }
    }
}
