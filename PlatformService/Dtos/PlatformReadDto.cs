using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Dtos
{
    public class PlatformReadDto
    {
        public string Name { get; set; } = default!;
        public string Publisher { get; set; } = default!;
        public string Cost { get; set; } = default!;
    }
}