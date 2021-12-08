using LoyaltyManagementSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string ImageList { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        public string Content { get; set; }
        [Required]
        [StringLength(250)]
        public string SKU { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
