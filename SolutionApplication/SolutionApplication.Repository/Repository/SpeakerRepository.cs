using Microsoft.EntityFrameworkCore;
using SolutionApplication.Database.Context;
using SolutionApplication.Database.DbModels;
using SolutionApplication.DTOs.Models;
using SolutionApplication.Repository.Interface;

namespace SolutionApplication.Repository.Repository
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly ApplicationDBContext _context;

        public SpeakerRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<SpeakerDTO>> GetSpeakers()
        {
            List<Speaker> speakerFromDB = await _context.Speakers.ToListAsync();

            List<SpeakerDTO> speakerDTO = speakerFromDB.Select(speaker => new SpeakerDTO()
            {
                SpeakerId = speaker.SpeakerId,
                SpeakerName = speaker.SpeakerName
            }).ToList();

            return speakerDTO;
        }

        public async Task<SpeakerDTO?> GetSpeakersById(int id)
        {
            Speaker? speakerFromDB = await _context.Speakers.FirstOrDefaultAsync(sp => sp.SpeakerId == id);

            SpeakerDTO speakerDTO;

            if (speakerFromDB != null)
            {
                speakerDTO = new SpeakerDTO()
                {
                    SpeakerId = speakerFromDB.SpeakerId,
                    SpeakerName = speakerFromDB.SpeakerName
                };

                return speakerDTO;
            }

            return null;
        }
    }
}
