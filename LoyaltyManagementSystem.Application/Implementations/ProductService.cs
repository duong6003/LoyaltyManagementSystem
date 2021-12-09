using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Data.Entities;
using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Infrastructure.Interfaces;
using LoyaltyManagementSystem.Ultilities.Dtos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<Inventory, int> _inventoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IRepository<Product, int> productRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IRepository<Inventory, int> inventoryRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
        }

        public ProductViewModel Create(ProductViewModel productVm)
        {
            Product product = _mapper.Map<ProductViewModel, Product>(productVm);
            _productRepository.Add(product);
            return productVm;
        }

        public void AddImages(int productId, string[] images)
        {
            Product product = _productRepository.FindById(productId);
            product.ImageList = string.Join(";", images);
        }

        public void Delete(int id)
        {
            var product = _productRepository.FindById(id);
            _productRepository.Remove(product);
        }

        public List<ProductViewModel> GetAll()
        {
            return _productRepository.FindAll().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public PageResult<ProductViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider).ToList();

            var paginationSet = new PageResult<ProductViewModel>()
            {
                Results = data,
                CurrentIndex = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public ProductViewModel GetById(int id)
        {
            return _mapper.Map<Product, ProductViewModel>(_productRepository.FindById(id));
        }

        public int GetQuantities(int productId)
        {
            int total = 0;
            _inventoryRepository.FindAll(x => x.ProductId == productId).ToList().ForEach(x=> total+=x.Quantity);
            return total;
        }

        public void ImportExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();

                    product.Name = workSheet.Cells[i, 1].Value.ToString();

                    product.Description = workSheet.Cells[i, 2].Value.ToString();

                    product.Content = workSheet.Cells[i, 3].Value.ToString();

                    product.SKU = workSheet.Cells[i, 4].Value.ToString();

                    product.Status = Status.Active;

                    _productRepository.Add(product);
                }
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductViewModel productVm)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productVm);
            product.DateModified = DateTime.Now;
            _productRepository.Update(product);
        }
    }
}
