using LoyaltyManagementSystem.Application.ViewModels.Promotion;
using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Interfaces
{
    public interface ICampaignService
    {
        List<CampaignViewModel> GetAll();
        PageResult<CampaignViewModel> GetAllPaging(string startDate, string endDate, string keyword, int index, int pageSize);
        void Create(CampaignViewModel collectionVm);

        void Update(int campaignId, List<CampaignDetailViewModel> campaignDetailsVm);
        void UpdateStatus(int campaignId, Status status);
        List<CampaignDetailViewModel> GetDetails(int campaignId);
        void CreateDetail(CampaignDetailViewModel collectionDetailVm);
        void DeleteDetail(int productId, int campaignId);

        void Delete(int id);

        CampaignViewModel GetById(int id);
        void Save();
    }
}
