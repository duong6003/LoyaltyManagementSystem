using LoyaltyManagementSystem.Application.Interfaces;
using LoyaltyManagementSystem.Application.ViewModels.Promotion;
using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.WebApi.Areas.Admin.Controllers
{
    public class CollectionController : ApiAdminController
    {
        private readonly ICollectionService _collectService;

        public CollectionController(ICollectionService collectService)
        {
            _collectService = collectService;
        }

        [HttpGet("get-all-details")]
        public IActionResult GetDetailsById(int collectionId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = _collectService.GetDetails(collectionId);

            return new OkObjectResult(model);
        }
        [HttpGet("get-all-active-collection")]
        public IActionResult GetAll()
        {
            var model = _collectService.GetAll();

            return new OkObjectResult(model);
        }
        [HttpPut("update-status")]
        public IActionResult UpdateStatus(int collectionId, Status status)
        {
            if (!ModelState.IsValid) return BadRequest();

            _collectService.UpdateStatus(collectionId, status);
            _collectService.Save();
            return Ok();
        }
        [HttpGet("get-all-collection-paging")]
        public IActionResult GetAllPaging(string startDate, string endDate, string keyword, int page, int pageSize)
        {
            var model = _collectService.GetAllPaging(startDate, endDate, keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpPost("add-collection")]
        public IActionResult AddEntity(CollectionViewModel collectionVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _collectService.Create(collectionVm);
            _collectService.Save();
            return Ok();
        }
        [HttpPut("update-details")]
        public IActionResult UpdateEntity(int collectionId , List<CollectionDetailViewModel> collectionDetailsVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _collectService.Update(collectionId,collectionDetailsVm);
            _collectService.Save();
            return Ok();
        }
        [HttpDelete("delete-collection")]
        public IActionResult Delete(int collectionId)
        {
            if (_collectService.GetById(collectionId) == null || !ModelState.IsValid) return BadRequest();
            _collectService.Delete(collectionId);
            _collectService.Save();
            return Ok();
        }
    }
}
