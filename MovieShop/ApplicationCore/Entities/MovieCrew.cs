using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieCrew
    {
        public int MovieId { get; set; }
        public int CrewId { get; set; }        
        [Required]
        public string Department { get; set; }
        [Required]
        public string Job { get; set; }

        public Movie Movie { get; set; }
        public Crew Crew { get; set; }
    }
}
