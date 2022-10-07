using System.ComponentModel.DataAnnotations;

namespace SolutionApplication.Database.DbModels
{
    public class Speaker
    {
        [Key]
        public int SpeakerId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "Max length is 100 caracteres")]
        public string SpeakerName { get; set; }
    }
}
