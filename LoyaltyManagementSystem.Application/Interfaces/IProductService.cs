using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Interfaces
{
    public interface IProductService
    {
        List<ProductViewModel> GetAll();
        PageResult<ProductViewModel> GetAllPaging(string keyword, int page, int pageSize);
        ProductViewModel Create(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        ProductViewModel GetById(int id);
        void ImportExcel(string filePath);

        void Save();

        int GetQuantities(int productId);

        void AddImages(int productId, string[] images);

    }
}
