using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Interfaces
{
    public interface IInventoryService
    {
        List<InventoryViewModel> GetAll();
        PageResult<InventoryViewModel> GetAllPaging(string keyword, int index, int pageSize);
        InventoryViewModel GetById(int inventoryId);
        void Create(InventoryViewModel inventory);
        void Update(InventoryViewModel inventory);
        void Delete(int id);
        void Save();
    }
}
