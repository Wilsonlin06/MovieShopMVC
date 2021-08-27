using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurhchaseRequestModel
    {
        [Required]
        public int userId { get; set; }
        public string purchaseNumber { get; set; }
        public double totalPrice { get; set; }
        public string purhcaseDateTime { get; set; }
        [Required]
        public int movieId { get; set; }
    }
}
