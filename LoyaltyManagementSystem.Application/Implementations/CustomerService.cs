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

namespace LoyaltyManagementSystem.Application.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly IRepository<Store, int> _storeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer, int> customerRepository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<Store, int> storeRepository)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storeRepository = storeRepository;
        }

        public void Create(CustomerViewModel customerVm)
        {
            Customer customer = _mapper.Map<CustomerViewModel, Customer>(customerVm);
            _customerRepository.Add(customer);
        }

        public void Delete(int customerId)
        {
            _customerRepository.Remove(customerId);
        }

        public PageResult<CustomerViewModel> GetAllPaging(string keyword, int index, int pageSize)
        {
            var query = _customerRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.CustomerName.Contains(keyword));

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateModified)
                .Skip((index - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider).ToList();

            var paginationSet = new PageResult<CustomerViewModel>()
            {
                Results = data,
                CurrentIndex = index,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public CustomerViewModel GetById(int id)
        {
            return _mapper.Map<Customer, CustomerViewModel>(_customerRepository.FindById(id));
        }

        public List<CustomerViewModel> GetByStore(int storeId)
        {
            var store = _storeRepository.FindById(storeId);
            return _mapper.Map<List<Customer>, List<CustomerViewModel>>(store.Customers.ToList());
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(CustomerViewModel customerVm)
        {
            var customer = _mapper.Map<CustomerViewModel, Customer>(customerVm);
            customer.DateModified = DateTime.Now;
            _customerRepository.Update(customer);
        }
    }
}