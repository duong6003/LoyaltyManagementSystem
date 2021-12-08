using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.System;
using LoyaltyManagementSystem.WebApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.WebApi.Areas.Admin.Controllers
{
    public class UserController : ApiAdminController
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserController(IUserService userService, IWebHostEnvironment hostingEnvironment = null)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAll()
        {
            var model = await _userService.GetAllAsync();
            return new OkObjectResult(model);
        }

        [HttpGet("get-users-by-pagination")]
        public IActionResult GetAllPaging(string keyword, int index, int pageSize)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = _userService.GetAllPaging(keyword, index, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = await _userService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost("add-or-update-entity")]
        public async Task<IActionResult> SaveEntity(AppUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(model);
            }
            if (model.Id == null)
            {
                await _userService.AddAsync(model);
            }
            else
            {
                await _userService.UpdateAsync(model);
            }
            return new OkObjectResult(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await _userService.DeleteAsync(id);                   

                return new OkObjectResult(id);
            }
        }

        [HttpPost("export-to-excel")]
        public async Task<IActionResult> ExportExcel()
        {
            string webRootFolder = _hostingEnvironment.WebRootPath;
            string directory = Path.Combine(webRootFolder, "export-files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string fileName = $"UserList_{DateTime.Now:yyyyMMddhhmmss}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(directory, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(webRootFolder, fileName));
            }
            var users = await _userService.GetAllAsync();
            //license excel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(file))
            {
                var sheet = package.Workbook.Worksheets.Add($"Danh sách User{DateTime.Now:d}");
                sheet.Cells["A1"].LoadFromCollection(users, true, TableStyles.Dark11);
                sheet.Cells.AutoFitColumns();

                // Save to file
                package.Save();
            }

            string fileUrl = $"{Request.Scheme}://{Request.Host}/export-files/{fileName}";
            return new OkObjectResult(fileUrl);
        }
    }
}