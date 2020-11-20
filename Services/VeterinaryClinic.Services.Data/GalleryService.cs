namespace VeterinaryClinic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class GalleryService : IGalleryService
    {
        private readonly IDeletableEntityRepository<Gallery> galleryRepository;

        public GalleryService(IDeletableEntityRepository<Gallery> galleryRepository)
        {
            this.galleryRepository = galleryRepository;
        }

        public IEnumerable<T> GetAllForAPage<T>(int page)
        {

            IQueryable<Gallery> query =
                this.galleryRepository.All()
                .OrderByDescending(x => x.CreatedOn)
            .Skip((page - 1) * GlobalConstants.PhotosOnOnePage)
                .Take(GlobalConstants.PhotosOnOnePage);

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var news = this.galleryRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return news;
        }

        public int GetCount()
        {
            return this.galleryRepository.AllAsNoTracking().Count();
        }
    }
}
