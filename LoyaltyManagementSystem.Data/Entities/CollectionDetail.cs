using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Data.Interfaces;
using LoyaltyManagementSystem.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Data.Entities
{
    [Table("CollectionDetails")]
    public class CollectionDetail : DomainEntity<int>
    {
        public CollectionDetail()
        {
        }

        public CollectionDetail(int collectionId, int productId, int storeId)
        {
            CollectionId = collectionId;
            ProductId = productId;
            StoreId = storeId;
        }

        public int CollectionId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
    }
}
