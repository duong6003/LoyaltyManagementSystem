using LoyaltyManagementSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.ViewModels.Product
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(128)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        public Status Status { get; set; }
        public ICollection<CustomerViewModel> Customers { get; set; }
    }
}
