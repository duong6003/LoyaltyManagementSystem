using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.Data.Entities;
using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Infrastructure.Interfaces;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IRepository<Store, int> _storeRepository;
        private readonly IRepository<Inventory, int> _inventoryRepository;
        private readonly IRepository<WareHouse, int> _wareHouseRepository;
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IRepository<Store, int> storeRepository, 
            IRepository<Inventory, int> inventoryRepository, 
            IRepository<WareHouse, int> wareHouseRepository, 
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _storeRepository = storeRepository;
            _inventoryRepository = inventoryRepository;
            _wareHouseRepository = wareHouseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public State CheckState(int productId, int wareHouseId)
        {
            Inventory inventory = _inventoryRepository.FindSingle(x => x.ProductId == productId && x.WareHouseId == wareHouseId);
            return inventory.ProductState;
        }

        public void Create(StoreViewModel storeVm)
        {
            var store = _mapper.Map<StoreViewModel, Store>(storeVm);
            _storeRepository.Add(store);
        }

        public void Delete(int storeId)
        {
            _storeRepository.Remove(storeId);
        }

        public List<StoreViewModel> GetAll()
        {
            var stores = _storeRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x=>x.Name);
            return stores.ProjectTo<StoreViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public List<CustomerViewModel> GetAllCustomer(int storeId)
        {
            var customers = _customerRepository.FindAll(x => x.StoreId == storeId);
            return customers.ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider).ToList();
        }

        public PageResult<StoreViewModel> GetAllPaging(string keyword, int index, int pageSize)
        {
            var query = _storeRepository.FindAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();

            var data = query.OrderByDescending(x=>x.Name).ProjectTo<StoreViewModel>(_mapper.ConfigurationProvider).ToList();

            var paginationSet = new PageResult<StoreViewModel>()
            {
                Results = data,
                CurrentIndex = index,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public List<WareHouseViewModel> GetAllWareHouse(int storeId)
        {
            var wareHouses = _wareHouseRepository.FindAll();
            var stores = _storeRepository.FindAll();
            var query = from w in wareHouses
                        join s in stores
                        on w.StoreId equals s.Id
                        where w.StoreId == storeId
                        select new WareHouseViewModel()
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Addresss = w.Addresss,
                            Country = w.Country,
                            Status = w.Status,
                            StoreId = w.StoreId
                        };
            return query.OrderByDescending(x=>x.Name).ToList();
        }

        public StoreViewModel GetById(int storeId)
        {
            return _mapper.Map<Store, StoreViewModel>(_storeRepository.FindById(storeId));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(StoreViewModel storeVm)
        {
            var store = _mapper.Map<StoreViewModel, Store>(storeVm);
            _storeRepository.Update(store);
        }

        public void UpdateQuantity(int productId, int wareHouseId, int quantity)
        {
            Inventory inventory = _inventoryRepository.FindSingle(x=> x.ProductId == productId && x.WareHouseId == wareHouseId);
            if (quantity > 0)
            {
                inventory.Quantity += quantity;
                inventory.ProductState = State.InStock;
            }
            else if (quantity < 0)
            {
                if(inventory.Quantity >= quantity)
                {
                    inventory.Quantity -= quantity;
                    if(inventory.Quantity == quantity)
                    {
                        inventory.ProductState = State.OutOfStock;
                    }
                }
            }
        }
    }
}
