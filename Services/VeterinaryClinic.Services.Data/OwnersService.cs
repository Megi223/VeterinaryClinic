﻿namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.Reviews;

    public class OwnersService : IOwnersService
    {
        private readonly IDeletableEntityRepository<Owner> ownersRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public OwnersService(IDeletableEntityRepository<Owner> ownersRepository, IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.ownersRepository = ownersRepository;
            this.reviewsRepository = reviewsRepository;
        }

        public async Task CreateOwnerAsync(ApplicationUser user, string firstName, string lastName, string profilePictureUrl, string city)
        {
            Owner owner = new Owner()
            {
                User = user,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = profilePictureUrl,
                City = city,
            };

            await this.ownersRepository.AddAsync(owner);
            await this.ownersRepository.SaveChangesAsync();
        }

        public string GetOwnerId(string userId)
        {
            return this.ownersRepository.AllAsNoTracking().Where(x => x.UserId == userId).FirstOrDefault().Id;
        }

        public async Task WriteReviewAsync(string ownerId, ReviewInputModel input)
        {
            Review review = new Review
            {
                Content = input.Content,
                OwnerId = ownerId,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public T GetById<T>(string id)
        {
            var owner = this.ownersRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return owner;
        }

        public async Task DeleteOwnerAsync(string userId)
        {
            var targetOwner = this.ownersRepository.All().Where(x => x.UserId == userId).FirstOrDefault();
            targetOwner.IsDeleted = true;
            await this.ownersRepository.SaveChangesAsync();
        }
    }
}
