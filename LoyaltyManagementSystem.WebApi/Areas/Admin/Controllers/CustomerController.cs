using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.WebApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class CustomerController : ApiAdminController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("get-customer-by-pagination")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _customerService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpGet("get-customer-by-id")]
        public IActionResult GetById(int id)
        {
            var model = _customerService.GetById(id);
            return new OkObjectResult(model);
        }

        [HttpPost("add-or-update-customer")]
        public IActionResult SaveEntity(CustomerViewModel customerVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (customerVm.Id == 0)
                {
                    _customerService.Create(customerVm);
                }
                else
                {
                    _customerService.Update(customerVm);
                }
                _customerService.Save();
                return new OkObjectResult(customerVm);
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_customerService.GetById(id) == null) return NotFound();

            _customerService.Delete(id);
            _customerService.Save();

            return new OkObjectResult(id);
        }

    }
}
