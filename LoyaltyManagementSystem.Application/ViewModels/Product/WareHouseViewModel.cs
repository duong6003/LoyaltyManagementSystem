using LoyaltyManagementSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.ViewModels.Product
{
    public class WareHouseViewModel
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Addresss { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
        public Status Status { get; set; }

        public int StoreId { get; set; }

        public StoreViewModel Store { get; set; }
    }
}
