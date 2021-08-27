using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class FavoriteRequestModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MovieId { get; set; }
    }
}
