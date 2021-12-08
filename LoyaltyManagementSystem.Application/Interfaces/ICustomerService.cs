using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System.Collections.Generic;

namespace LoyaltyManagementSystem.Application.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetByStore(int storeId);
        CustomerViewModel GetById(int id);
        PageResult<CustomerViewModel> GetAllPaging(string keyword, int index, int pageSize);
        void Create(CustomerViewModel customerVm);
        void Update(CustomerViewModel customerVm);
        void Delete(int customerId);
        void Save();
    }
}