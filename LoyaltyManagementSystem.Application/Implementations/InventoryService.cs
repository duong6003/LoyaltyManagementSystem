using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Data.Entities;
using LoyaltyManagementSystem.Infrastructure.Interfaces;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory, int> _inventoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IRepository<Inventory, int> inventoryRepository, 
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(InventoryViewModel inventory)
        {
            _inventoryRepository.Add(_mapper.Map<InventoryViewModel, Inventory>(inventory));
        }

        public void Delete(int id)
        {
            var inventory = _inventoryRepository.FindById(id);
            _inventoryRepository.Remove(inventory);
        }

        public List<InventoryViewModel> GetAll()
        {
            return _inventoryRepository.FindAll().ProjectTo<InventoryViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public PageResult<InventoryViewModel> GetAllPaging(string keyword, int index, int pageSize)
        {
            var query = _inventoryRepository.FindAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.WareHouse.Name.Contains(keyword)||x.Product.Name.Contains(keyword));
            }
            int totalRow = query.Count();
            query = query.OrderByDescending(x => x.DateModified).Skip((index - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<InventoryViewModel>(_mapper.ConfigurationProvider).ToList();
            var paginationSet = new PageResult<InventoryViewModel>()
            {
                Results = data,
                CurrentIndex = index,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public InventoryViewModel GetById(int inventoryId)
        {
            return _mapper.Map<Inventory, InventoryViewModel>(_inventoryRepository.FindById(inventoryId));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(InventoryViewModel inventoryVm)
        {
            var inventory = _mapper.Map<InventoryViewModel, Inventory>(inventoryVm);
            inventory.DateModified = DateTime.Now;
            _inventoryRepository.Update(inventory);
        }
    }
}
