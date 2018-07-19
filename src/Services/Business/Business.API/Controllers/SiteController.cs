﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.Business.API.Requests;
using SaaSEqt.eShop.Services.Business.Infrastructure;
using SaaSEqt.eShop.Services.Business.Model;
using SaaSEqt.eShop.Services.Business.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SiteController : Controller
    {
        private BusinessService _businessService;

        public SiteController(BusinessService businessService)
        {
            this._businessService = businessService;
        }

        [HttpGet]
        [Route("sites/{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Site), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSiteById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var site = await _businessService.FindExistingSite(id);

            if (site != null)
            {
                return Ok(site);
            }

            return NotFound();
        }

        // POST api/v1/[controller]/ProvisionSite
        [HttpPost]
        [Route("ProvisionSite")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProvisionSite([FromBody]ProvisionSiteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)BadRequest();
            }

            //byte[] logo;
            //using (var memoryStream = new MemoryStream())
            //{
            //    request.Logo.CopyTo(memoryStream);
            //    logo = memoryStream.ToArray();
            //}

            Site site = new Site(request.TenantId, request.Name, request.Description, false);

            await _businessService.ProvisionSite(site);

            return CreatedAtAction(nameof(GetSiteById), new { id = site.Id }, null);
        }
    }
}
