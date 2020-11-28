﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Vets;

namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    [Authorize(Roles = GlobalConstants.VetRoleName)]
    [Area("Vet")]
    public class VetController : Controller
    {
        private readonly IVetsService vetsService;
        private readonly IPaginatedMetaService paginatedMetaService;

        public VetController(IVetsService vetsService, IPaginatedMetaService paginatedMetaService)
        {
            this.vetsService = vetsService;
            this.paginatedMetaService = paginatedMetaService;
        }

        [AllowAnonymous]
        public IActionResult All(int id = 1)
        {
            var vetsCount = this.vetsService.GetCount();
            var allPagesCount = (vetsCount / GlobalConstants.VetsOnOnePage) + 1;
            if (id < 1)
            {
                return this.RedirectToAction("All", new { id = 1 });
            }
            else if (id > allPagesCount)
            {
                return this.RedirectToAction("All", new { id = allPagesCount });
            }
            var viewModel = this.vetsService.GetAllForAPage<VetsAllViewModel>(id);
            foreach (var vetModel in viewModel)
            {
                vetModel.Services = this.vetsService.GetServices(vetModel.Id);
            }
            this.ViewBag.PaginatedMeta = this.paginatedMetaService.GetMetaData(vetsCount, id, GlobalConstants.VetsOnOnePage);
            return this.View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var viewModel = this.vetsService.GetById<VetViewModel>(id);
            return this.View(viewModel);
        }
    }
}
