using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data
{
    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public IEnumerable<T> GetAllForAPage<T>(int page)
        {
            IQueryable<Review> query =
                this.reviewsRepository.All()
                .OrderByDescending(x=>x.CreatedOn)
            .Skip((page - 1) * GlobalConstants.ReviewsOnOnePage)
                .Take(GlobalConstants.ReviewsOnOnePage);

            return query.To<T>().ToList();
        }

        public int GetCount()
        {
            return this.reviewsRepository.AllAsNoTracking().Count();
        }
    }
}
