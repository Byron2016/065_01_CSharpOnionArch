using Microsoft.AspNetCore.Mvc;
using SolutionApplication.DTOs.Models;
using SolutionApplication.Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakerController(ISpeakerRepository speakerRepository)
        {
            _speakerRepository = speakerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpeakers()
        {
            List<SpeakerDTO> speakerFromRepository = await _speakerRepository.GetSpeakers();

            return Ok(speakerFromRepository);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetSpeakersById(int id)
        {
            SpeakerDTO speakerFromRepository = await _speakerRepository.GetSpeakersById(id);

            if (speakerFromRepository != null)
            {
                return Ok(speakerFromRepository);
            }

            return NotFound();
        }
    }
}
