using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.ViewModels.Product
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        [Required]
        [StringLength(128)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(250)]
        public string CustomerAddress { get; set; }
        [Required]
        [StringLength(10)]
        public string CustomerPhone { get; set; }
        [Required]
        [StringLength(50)]
        public string RedemptionCode { get; set; }
        [StringLength(128)]
        public string Gift { get; set; }
        public DateTime DayOfRedemption { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
