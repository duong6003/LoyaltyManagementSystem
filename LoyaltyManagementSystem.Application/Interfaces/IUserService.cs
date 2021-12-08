using LoyaltyManagementSystem.Application.ViewModels.System;
using LoyaltyManagementSystem.Ultilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);

        Task<List<AppUserViewModel>> GetAllAsync();

        PageResult<AppUserViewModel> GetAllPaging(string keyword, int index, int pageSize);

        Task<AppUserViewModel> GetById(string id);

        Task UpdateAsync(AppUserViewModel userVm);
    }
}