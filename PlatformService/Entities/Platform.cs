using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Entities
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Publisher { get; set; } = default!;

        [Required]
        public string Cost { get; set; } = default!;
    }
}