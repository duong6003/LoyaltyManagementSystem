using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Interfaces
{
    public interface IStoreService
    {
        List<StoreViewModel> GetAll();
        List<CustomerViewModel> GetAllCustomer(int storeId);
        List<WareHouseViewModel> GetAllWareHouse(int storeId);
        State CheckState(int productId, int wareHouseId);
        void Save();
        void Create(StoreViewModel storeVm);
        void Update(StoreViewModel storeVm);
        StoreViewModel GetById(int storeId);
        void Delete(int storeId);
        PageResult<StoreViewModel> GetAllPaging(string keyword, int index, int pageSize);
        void UpdateQuantity(int productId, int wareHouseId ,int quantity);
    }
}
