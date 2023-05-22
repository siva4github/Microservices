
using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        public string HowTo { get; set; } = default!;
        public string CommandLine { get; set; } = default!;
    }
}