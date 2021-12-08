using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.WebApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.WebApi.Areas.Admin.Controllers
{
    public class InventoryController : ApiAdminController
    {
        private readonly IInventoryService _inventoryService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public InventoryController(IInventoryService inventoryService, IWebHostEnvironment hostEnvironment)
        {
            _inventoryService = inventoryService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("get-all-inventories")]
        public IActionResult GetAll()
        {
            var model = _inventoryService.GetAll();
            return new OkObjectResult(model);
        }
        [HttpGet("get-inventories-by-pagination")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _inventoryService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpGet("get-product-by-id")]
        public IActionResult GetById(int id)
        {
            if (_inventoryService.GetById(id) == null) return NotFound();
            var model = _inventoryService.GetById(id);
            return new OkObjectResult(model);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int inventoryId)
        {
            if (_inventoryService.GetById(inventoryId) == null) return NotFound();
            _inventoryService.Delete(inventoryId);
            _inventoryService.Save();
            return new OkObjectResult(inventoryId);
        }

        [HttpPost("add-or-update-inventory")]
        public IActionResult SaveEntity(InventoryViewModel inventoryVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (inventoryVm.Id == 0)
                {
                    _inventoryService.Create(inventoryVm);
                }
                else
                {
                    _inventoryService.Update(inventoryVm);
                }
                _inventoryService.Save();
                return new OkObjectResult(inventoryVm);
            }
        }
    }
}
