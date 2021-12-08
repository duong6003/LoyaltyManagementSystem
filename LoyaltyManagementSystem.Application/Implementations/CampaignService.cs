using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Promotion;
using LoyaltyManagementSystem.Data.Entities;
using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Infrastructure.Interfaces;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Implementations
{
    public class CampaignService : ICampaignService
    {
        private readonly IRepository<Campaign, int> _campaignRepository;
        private readonly IRepository<CampaignDetail, int> _campaignDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWOrk;

        public CampaignService(IRepository<Campaign, int> campaignRepository, 
            IRepository<CampaignDetail, int> campaignDetailRepository, 
            IMapper mapper, IUnitOfWork unitOfWOrk)
        {
            _campaignRepository = campaignRepository;
            _campaignDetailRepository = campaignDetailRepository;
            _mapper = mapper;
            _unitOfWOrk = unitOfWOrk;
        }

        public void Create(CampaignViewModel campaignVm)
        {
            Campaign campaign = _mapper.Map<CampaignViewModel, Campaign>(campaignVm);
            _campaignRepository.Add(campaign);
        }

        public void Delete(int id)
        {
            var campaign = _campaignRepository.FindById(id);
            var campaignDetails = _campaignDetailRepository.FindAll(x => x.CampaignId == id).ToList();
            _campaignRepository.Remove(campaign);
            _campaignDetailRepository.RemoveMultiple(campaignDetails);
        }
        public void UpdateStatus(int campaignId, Status status)
        {
            var campaign = _campaignRepository.FindById(campaignId);
            campaign.Status = status;
            _campaignRepository.Update(campaign);
        }

        public List<CampaignViewModel> GetAll()
        {
            return _campaignRepository.FindAll().ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public PageResult<CampaignViewModel> GetAllPaging(string startDate, string endDate, string keyword, int index, int pageSize)
        {
            var query = _campaignRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.StartDate >= start);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.EndDate <= end);
            }
            int totalRow = query.Count();
            query = query.Skip((index - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<CampaignViewModel>(_mapper.ConfigurationProvider).ToList();
            var paginationSet = new PageResult<CampaignViewModel>()
            {
                Results = data,
                CurrentIndex = index,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public CampaignViewModel GetById(int id)
        {
            var campaign = _campaignRepository.FindById(id);
            var details = _campaignDetailRepository.FindAll(x => x.CampaignId == id);
            campaign.CampaignDetails = details.ToList();
            return _mapper.Map<Campaign, CampaignViewModel>(campaign);
        }

        public void Save()
        {
            _unitOfWOrk.Commit();
        }

        public List<CampaignDetailViewModel> GetDetails(int campaignId)
        {
            Campaign campaign = _campaignRepository.FindById(campaignId);
            var details = campaign.CampaignDetails.ToList();
            return _mapper.Map<List<CampaignDetail>, List<CampaignDetailViewModel>>(details).ToList();
        }

        public void CreateDetail(CampaignDetailViewModel campaignDetailVm)
        {
            var campaignDetail = _mapper.Map<CampaignDetailViewModel, CampaignDetail>(campaignDetailVm);
            _campaignDetailRepository.Add(campaignDetail);
        }

        public void DeleteDetail(int productId, int campaignId)
        {
            var detail = _campaignDetailRepository.FindSingle(x => x.ProductId == productId
           && x.CampaignId == campaignId);
            _campaignDetailRepository.Remove(detail);
        }

        public void Update(int campaignId, List<CampaignDetailViewModel> campaignDetailsVm)
        {
            var campaign = _campaignRepository.FindById(campaignId);
            var oldDetails = _campaignDetailRepository.FindAll(x => x.CampaignId == campaignId);
            var newDetails = campaignDetailsVm.AsQueryable().ProjectTo<CampaignDetail>(_mapper.ConfigurationProvider).ToList();
            var addedDetails = newDetails.Where(x => x.Id == 0).ToList();
            var updatedDetails = newDetails.Where(x => x.CampaignId == campaignId).ToList();
            foreach (var detail in addedDetails)
            {
                _campaignDetailRepository.Add(detail);
            }
            foreach (var detail in updatedDetails)
            {
                _campaignDetailRepository.Update(detail);
            }
        }
    }
}

