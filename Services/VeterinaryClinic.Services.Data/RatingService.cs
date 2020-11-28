using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;

namespace VeterinaryClinic.Services.Data
{
    public class RatingService : IRatingService
    {
        private readonly IDeletableEntityRepository<Rating> ratingsRepository;

        public RatingService(IDeletableEntityRepository<Rating> ratingsRepository)
        {
            this.ratingsRepository = ratingsRepository;
        }

        public float GetAverageRatings(string vetId)
        {
            return this.ratingsRepository.All()
                .Where(x => x.VetId == vetId)
                .Average(x => x.Score);
        }

        public async Task SetRatingAsync(string vetId, string ownerId, byte score)
        {
            var rating = this.ratingsRepository.All()
                .FirstOrDefault(x => x.VetId == vetId && x.OwnerId == ownerId);
            if (rating == null)
            {
                rating = new Rating
                {
                    VetId = vetId,
                    OwnerId = ownerId,
                };

                await this.ratingsRepository.AddAsync(rating);
            }

            rating.Score = score;
            await this.ratingsRepository.SaveChangesAsync();
        }
    }
}
