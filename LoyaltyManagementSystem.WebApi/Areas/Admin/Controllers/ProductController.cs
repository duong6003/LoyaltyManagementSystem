using AutoMapper.Configuration;
using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Product;
using LoyaltyManagementSystem.WebApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.WebApi.Areas.Admin.Controllers
{
    public class ProductController : ApiAdminController
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("get-all-product")]
        public IActionResult GetAll()
        {
            var model = _productService.GetAll();
            return new OkObjectResult(model);
        }
        [HttpGet("get-product-by-pagination")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _productService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpGet("get-product-by-id")]
        public IActionResult GetById(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = _productService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost("add-or-update-product")]
        public IActionResult SaveEntity(ProductViewModel productVm)
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
                    _productService.Create(productVm);
                }
                else
                {
                    _productService.Update(productVm);
                }
                _productService.Save();
                return new OkObjectResult(productVm);
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_productService.GetById(id) == null) return NotFound();

            _productService.Delete(id);
                _productService.Save();

                return new OkObjectResult(id);
        }
        [HttpGet("get-total-quantities")]
        public IActionResult GetQuantities(int productId)
        {
            int quantities = _productService.GetQuantities(productId);
            return new OkObjectResult(quantities);
        }
        [HttpPost("add-image")]
        public IActionResult SaveImages(int productId, string[] images)
        {
            if (_productService.GetById(productId) == null) return NotFound();
            _productService.AddImages(productId, images);
            _productService.Save();
            return new OkObjectResult(images);
        }
        [HttpPost("import-product-by-excel")]
        public IActionResult ImportExcel(IList<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var filename = ContentDispositionHeaderValue
                                   .Parse(file.ContentDisposition)
                                   .FileName
                                   .Trim('"');

                string folder = _hostingEnvironment.WebRootPath + $@"\uploaded\excels";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filePath = Path.Combine(folder, filename);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                _productService.ImportExcel(filePath);
                _productService.Save();
                return new OkObjectResult(filePath);
            }
            return new NoContentResult();
        }
    }
}
