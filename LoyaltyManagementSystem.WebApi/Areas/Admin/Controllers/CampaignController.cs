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
    public class CampaignController : ApiAdminController
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("get-select-campaign")]
        public IActionResult GetById(int campaignId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var model = _campaignService.GetById(campaignId);

            return new OkObjectResult(model);
        }
        [HttpGet("get-all-active-campaign")]
        public IActionResult GetAll()
        {
            string token = HttpContext.Session.GetString("Token");
            var model = _campaignService.GetAll();

            return new OkObjectResult(model);
        }
        [HttpPut("update-status")]
        public IActionResult UpdateStatus(int campaignId, Status status)
        {
            if (!ModelState.IsValid) return BadRequest();
                _campaignService.UpdateStatus(campaignId, status);
            _campaignService.Save();
            return Ok();
        }
        [HttpGet("get-all-campaign-paging")]
        public IActionResult GetAllPaging(string startDate, string endDate, string keyword, int page, int pageSize)
        {
            var model = _campaignService.GetAllPaging(startDate, endDate, keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpPost("add-campaign")]
        public IActionResult AddEntity(CampaignViewModel campaignVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _campaignService.Create(campaignVm);
            _campaignService.Save();
            return Ok();
        }
        [HttpPut("update-details")]
        public IActionResult UpdateEntity(int campaignId, List<CampaignDetailViewModel> campaignDetailsVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _campaignService.Update(campaignId, campaignDetailsVm);
            _campaignService.Save();
            return Ok();
        }
        [HttpDelete("delete-campaign")]
        public IActionResult Delete([FromQuery]int campaignId)
        {
            if (!ModelState.IsValid) return BadRequest();

            _campaignService.Delete(campaignId);
            _campaignService.Save();
            return Ok();
        }
    }
}
