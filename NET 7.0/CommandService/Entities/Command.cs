using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CommandService.Entities
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string HowTo { get; set; } = default!;
        [Required]
        public string CommandLine { get; set; } = default!;
        [Required]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; } = default!;
    }
}