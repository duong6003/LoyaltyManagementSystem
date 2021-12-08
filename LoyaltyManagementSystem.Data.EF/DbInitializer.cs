using LoyaltyManagementSystem.Data.Entities;
using LoyaltyManagementSystem.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    Avatar = "",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123456");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            if (_context.Functions.Count() == 0)
            {
                await _context.Functions.AddRangeAsync(new List<Function>()
                {
                     new Function() {Id = "SYSTEM", Name = "System",ParentId = null,SortOrder = 1,Status = Status.Active,Url = "/" },
                     new Function() {Id = "ROLE", Name = "Role",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,Url = "/admin/role/index"},
                     new Function() {Id = "FUNCTION", Name = "Function",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,Url = "/admin/function/index" },
                     new Function() {Id = "USER", Name = "User",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,Url = "/admin/user/index" },

                     new Function() {Id = "PRODUCT",Name = "Product Management",ParentId = null,SortOrder = 2,Status = Status.Active,Url = "/" },
                     new Function() {Id = "ALLPRODUCT",Name = "All Product",ParentId = "PRODUCT", SortOrder = 1,Status = Status.Active,Url = "/admin/product/index" },
                     new Function() {Id = "INVENTORY",Name = "Inventory",ParentId = "PRODUCT", SortOrder = 2,Status = Status.Active,Url = "/admin/inventory/index" },

                     new Function() {Id = "PROMOTION",Name = "Promotion Management",ParentId = null,SortOrder = 3,Status = Status.Active,Url = "/" },
                     new Function() {Id = "COLLECTION",Name = "Collection",ParentId = "PROMOTION",SortOrder = 1,Status = Status.Active,Url = "/admin/collection/index" },
                     new Function() {Id = "CAMPAIGN",Name = "Campaign",ParentId = "PROMOTION",SortOrder = 2,Status = Status.Active,Url = "/admin/campaign/index" },
                     new Function() {Id = "STORE",Name = "Store",ParentId = "PROMOTION",SortOrder = 3,Status = Status.Active,Url = "/admin/store/index" },

                     new Function() {Id = "CUSTOMER",Name = "Customer",ParentId = null,SortOrder = 4,Status = Status.Active,Url = "/admin/customer/index" }
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
