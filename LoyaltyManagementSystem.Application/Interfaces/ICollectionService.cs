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
    public interface ICollectionService
    {
        List<CollectionViewModel> GetAll();
        PageResult<CollectionViewModel> GetAllPaging(string startDate, string endDate, string keyword, int index, int pageSize);
        void Create(CollectionViewModel collectionVm);

        void Update(int collectionId, List<CollectionDetailViewModel> collectionDetailsVm);
        void UpdateStatus(int collectionId, Status status);
        List<CollectionDetailViewModel> GetDetails(int collectionId);
        void CreateDetail(CollectionDetailViewModel collectionDetailVm);
        void DeleteDetail(int productId, int collectionId);

        void Delete(int id);

        CollectionViewModel GetById(int id);
        void Save();
    }
}
