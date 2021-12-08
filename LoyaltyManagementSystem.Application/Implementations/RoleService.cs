using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.System;
using LoyaltyManagementSystem.Data.Entities;
using LoyaltyManagementSystem.Infrastructure.Interfaces;
using LoyaltyManagementSystem.Ultilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private IRepository<Function, string> _functionRepository;
        private IRepository<Permission, int> _permissionRepository;

        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(AppRoleViewModel userVm)
        {
            var role = new AppRole()
            {
                Name = userVm.Name,
                Description = userVm.Description
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public Task<bool> CheckPermission(string functionId, string action, string[] roles)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Id ==functionId
                        && ((p.CanCreate && action == "Create")
                        || (p.CanUpdate && action == "Update")
                        || (p.CanDelete && action == "Delete")
                        || (p.CanRead && action == "Read"))
                        select p;
            return query.AnyAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await GetById(id);
            await _roleManager.DeleteAsync(_mapper.Map<AppRoleViewModel,AppRole>(role));
        }

        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles.ProjectTo<AppRoleViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public PageResult<AppRoleViewModel> GetAllPaging(string keyword, int index, int pageSize)
        {
            var query = _roleManager.Roles;
            if (string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            int totalRow = query.Count();
            query = query.Skip((index - 1) * pageSize)
               .Take(pageSize);

            var data = query.Select(x => new AppRoleViewModel()
            {
                Name = x.Name,
                Description = x.Description
            }).ToList();
            var paginationSet = new PageResult<AppRoleViewModel>()
            {
                Results = data,
                CurrentIndex = index,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public async Task<AppRoleViewModel> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return _mapper.Map<AppRole,AppRoleViewModel>(role);
        }

        public List<PermissionViewModel> GetListFunctionWithRole(Guid roleId)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();

            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId into fp
                        from p in fp.DefaultIfEmpty()
                        where p != null && p.RoleId == roleId
                        select new PermissionViewModel()
                        {
                            RoleId = roleId,
                            FunctionId = f.Id,
                            CanCreate = p != null ? p.CanCreate : false,
                            CanDelete = p != null ? p.CanDelete : false,
                            CanRead = p != null ? p.CanRead : false,
                            CanUpdate = p != null ? p.CanUpdate : false
                        };
            return query.ToList();
        }

        public void SavePermission(List<PermissionViewModel> permissionsVm, Guid roleId)
        {
            var permissions = _mapper.Map<List<PermissionViewModel>, List<Permission>>(permissionsVm);
            var oldPermissions = _permissionRepository.FindAll(x => x.RoleId == roleId).ToList();
            if(oldPermissions.Count > 0)
            {
                _permissionRepository.RemoveMultiple(oldPermissions);
                
            }
            foreach (var permission in permissions)
            {
                _permissionRepository.Add(permission);
            }
            _unitOfWork.Commit();
        }

        public async Task UpdateAsync(AppRoleViewModel roleVm)
        {
            var role = await _roleManager.FindByIdAsync(roleVm.Id.ToString());
            role.Name = roleVm.Name;
            role.Description = roleVm.Description;
            await _roleManager.UpdateAsync(role);
        }
    }
}
