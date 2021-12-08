using LoyaltyManagementSystem.Application.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyManagementSystem.Application.ViewModels.Promotion
{
    public class CampaignDetailViewModel
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }

        public int CollectionId { get; set; }

        public int ProductId { get; set; }
        public int StoreId { get; set; }

    }
}