namespace VeterinaryClinic.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Pioneer.Pagination;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Gallery;

    public class GalleryController : Controller
    {
        private readonly IGalleryService galleryService;
        private readonly IPaginatedMetaService paginatedMetaService;

        public GalleryController(IGalleryService galleryService, IPaginatedMetaService paginatedMetaService)
        {
            this.galleryService = galleryService;
            this.paginatedMetaService = paginatedMetaService;
        }

        public IActionResult All(int id)
        {
            var photosCount = this.galleryService.GetCount();
            var photosOnPage = GlobalConstants.PhotosOnOnePage;
            var allPagesCount = (photosCount / photosOnPage) + 1;
            if (id < 1)
            {
                return this.RedirectToAction("All", new { id = 1 });
            }
            else if (id > allPagesCount)
            {
                return this.RedirectToAction("All", new { id = allPagesCount });
            }

            var viewModel = this.galleryService.GetAllForAPage<GalleryAllViewModel>(id);
            this.ViewBag.PaginatedMeta = this.paginatedMetaService.GetMetaData(photosCount, id, photosOnPage);
            return this.View(viewModel);
        }
    }
}
