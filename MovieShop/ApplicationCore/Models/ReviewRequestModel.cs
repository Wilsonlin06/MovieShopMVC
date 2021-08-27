using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ReviewRequestModel
    {
        [Required]
        public int movieId { get; set; }
        [Required]
        public int userId { get; set; }
        public string reviewText { get; set; }
        [Required]
        public decimal Rating { get; set; }

    }
}
