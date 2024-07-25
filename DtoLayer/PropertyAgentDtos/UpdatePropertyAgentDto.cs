using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.PropertyAgentDtos
{
    public class UpdatePropertyAgentDto
    {
        public int PropertyAgentID { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
    }
}
