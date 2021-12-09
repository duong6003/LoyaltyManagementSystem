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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Implementations
{
    public class CollectionService : ICollectionService
    {
        private readonly IRepository<Collection, int> _collectionRepository;
        private readonly IRepository<CollectionDetail, int> _collectionDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWOrk;

        public CollectionService(IRepository<Collection, int> collectionRepository,
            IRepository<CollectionDetail, int> collectionDetailRepository,
            IMapper mapper, IUnitOfWork unitOfWOrk)
        {
            _collectionRepository = collectionRepository;
            _mapper = mapper;
            _unitOfWOrk = unitOfWOrk;
            _collectionDetailRepository = collectionDetailRepository;
        }

        public void Create(CollectionViewModel collectionVm)
        {
            Collection collection = _mapper.Map<CollectionViewModel, Collection>(collectionVm);
            _collectionRepository.Add(collection);
        }

        public void Delete(int id)
        {
            var collection = _collectionRepository.FindById(id);
            var collectionDetais = _collectionDetailRepository.FindAll(x => x.CollectionId == id).ToList();
            _collectionRepository.Remove(collection);
            _collectionDetailRepository.RemoveMultiple(collectionDetais);
        }
        public void UpdateStatus(int collectionId, Status status)
        {
            var collection = _collectionRepository.FindById(collectionId);
            collection.Status = status;
            _collectionRepository.Update(collection);
        }

        public List<CollectionViewModel> GetAll()
        {
            return _collectionRepository.FindAll(x=>x.Status==Status.Active).ProjectTo<CollectionViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public PageResult<CollectionViewModel> GetAllPaging(string startDate, string endDate, string keyword, int index, int pageSize)
        {
            var query = _collectionRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x=>x.Name.Contains(keyword)||x.Description.Contains(keyword));
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

            var data = query.ProjectTo<CollectionViewModel>(_mapper.ConfigurationProvider).ToList();
            var paginationSet = new PageResult<CollectionViewModel>()
            {
                Results = data,
                CurrentIndex = index,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public CollectionViewModel GetById(int id)
        {
            var collection = _collectionRepository.FindById(id);
            var details = _collectionDetailRepository.FindAll(x => x.CollectionId == id);
            collection.CollectionDetails = details.ToList();
            return _mapper.Map<Collection, CollectionViewModel>(collection);
        }

        public void Save()
        {
            _unitOfWOrk.Commit();
        }

        public void Update(int collectionId , List<CollectionDetailViewModel> collectionDetailVm)
        {
            var collection = _collectionRepository.FindById(collectionId);
            var oldDetails = _collectionDetailRepository.FindAll(x => x.CollectionId == collectionId);
            var newDetails = collectionDetailVm.AsQueryable().ProjectTo<CollectionDetail>(_mapper.ConfigurationProvider).ToList();
            var addedDetails = newDetails.Where(x => x.Id == 0).ToList();
            var updatedDetails = newDetails.Where(x => x.Id == collectionId).ToList();
            foreach (var detail in addedDetails)
            {
                _collectionDetailRepository.Add(detail);
            }
            foreach (var detail in updatedDetails)
            {
                _collectionDetailRepository.Update(detail);
            }
        }

        public List<CollectionDetailViewModel> GetDetails(int collectionId)
        {
            var details = _collectionDetailRepository.FindAll(x => x.CollectionId == collectionId);
            return details.ProjectTo<CollectionDetailViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public void CreateDetail(CollectionDetailViewModel collectionDetailVm)
        {
            var billDetail = _mapper.Map<CollectionDetailViewModel, CollectionDetail>(collectionDetailVm);
            _collectionDetailRepository.Add(billDetail);
        }

        public void DeleteDetail(int productId, int collectionId)
        {
            var detail = _collectionDetailRepository.FindSingle(x => x.ProductId == productId
           && x.CollectionId == collectionId);
            _collectionDetailRepository.Remove(detail);
        }
    }
}
