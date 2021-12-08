using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.WebApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.WebApi.Areas.Admin.Controllers
{
    public class StoreController : ApiAdminController
    {
        private readonly IStoreService _storeService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductService _productService;

        public StoreController(IStoreService storeService, IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _storeService = storeService;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("get-all-store")]
        public IActionResult GetAll()
        {
            var model = _storeService.GetAll();
            return new OkObjectResult(model);
        }
        [HttpGet("get-warehouse-of-store")]
        public IActionResult GetListWareHouse(int storeId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = _storeService.GetAllWareHouse(storeId);
            return new OkObjectResult(model);
        }
        [HttpGet("get-select-store")]
        public IActionResult GetById(int storeId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = _storeService.GetById(storeId);

            return new OkObjectResult(model);
        }

        [HttpGet("get-all-store-paging")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = _storeService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpPost("add-or-update-store")]
        public IActionResult SaveEntity(StoreViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (productVm.Id == 0)
                {
                    _storeService.Create(productVm);
                }
                else
                {
                    _storeService.Update(productVm);
                }
                _storeService.Save();
                return new OkObjectResult(productVm);
            }
        }
        [HttpPut("update-quantity-of-product")]
        public IActionResult UpdateQuantity(int productId, int wareHouseId, int quantity)
        {
            if (_productService.GetById(productId) == null || !ModelState.IsValid) return BadRequest();
            _storeService.UpdateQuantity(productId, wareHouseId, quantity);
            _storeService.Save();
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_storeService.GetById(id) == null || !ModelState.IsValid) return BadRequest();

            _storeService.Delete(id);
            _storeService.Save();

            return Ok();
        }
        [HttpPost("export-to-excel-for-store")]
        public IActionResult ExportExcel(int storeId)
        {
            var store = _storeService.GetById(storeId);
            string webRootFolder = _hostingEnvironment.WebRootPath;
            string directory = Path.Combine(webRootFolder, "export-files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string fileName = $"CustomerList_{DateTime.Now:yyyyMMddhhmmss}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(directory, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(webRootFolder, fileName));
            }
            var cumtomers = _storeService.GetAllCustomer(storeId);
            //license excel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(file))
            {
                var sheet = package.Workbook.Worksheets.Add($"Danh sách Customer cửa hàng {store.Name}");
                sheet.Cells["A1"].LoadFromCollection(cumtomers, true, TableStyles.Dark11);
                sheet.Cells.AutoFitColumns();

                // Save to file
                package.Save();
            }

            string fileUrl = $"{Request.Scheme}://{Request.Host}/export-files/{fileName}";
            return new OkObjectResult(fileUrl);
        }
    }
}
