using SolutionApplication.DTOs.Models;

namespace SolutionApplication.Repository.Interface
{
    public interface ISpeakerRepository
    {
        Task<List<SpeakerDTO>> GetSpeakers();
        Task<SpeakerDTO> GetSpeakersById(int id);
    }
}
