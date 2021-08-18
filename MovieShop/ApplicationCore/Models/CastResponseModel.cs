using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
   public class CastResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ProfilePath { get; set; }
        public string Character { get; set; }

        public List<MovieDetailsResponseModel> Movies { get; set; }
    }
}
