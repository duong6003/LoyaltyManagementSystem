using LoyaltyManagementSystem.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoyaltyManagementSystem.Data.Entities
{
    [Table("CampaignDetails")]
    public class CampaignDetail : DomainEntity<int>
    {
        public CampaignDetail()
        {
        }

        public CampaignDetail(int campaignId, int collectionId, 
            int productId, int storeId)
        {
            CampaignId = campaignId;
            CollectionId = collectionId;
            ProductId = productId;
            StoreId = storeId;
        }

        public int CampaignId { get; set; }

        [MaxLength(50)]
        public int CollectionId { get; set; }

        public int ProductId { get; set; }
        public int StoreId { get; set; }

        [ForeignKey("CampaignId")]
        public virtual Campaign Campaign { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
    }
}