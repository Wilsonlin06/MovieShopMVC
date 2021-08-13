using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    // 56 => name of trailer, url, Title, Revenue
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        // ForeignKey
        public int MovieId { get; set; }
        
        // Navigation Property
        public Movie Movie { get; set; }
    }
}
