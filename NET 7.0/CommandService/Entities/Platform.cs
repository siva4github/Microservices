using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CommandService.Entities
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalId { get; set; }
        [Required]
        public string Name { get; set; }= default!;
        public ICollection<Command> Commands { get; set; }= default!;
    }
}