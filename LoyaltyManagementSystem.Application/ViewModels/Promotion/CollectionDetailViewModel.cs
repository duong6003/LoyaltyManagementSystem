using LoyaltyManagementSystem.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.ViewModels.Promotion
{
    public class CollectionDetailViewModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public int CollectionId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }

    }
}
