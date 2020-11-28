﻿namespace VeterinaryClinic.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;

    public class OwnersService : IOwnersService
    {
        private readonly IDeletableEntityRepository<Owner> ownersRepository;


        public OwnersService(IDeletableEntityRepository<Owner> ownersRepository)
        {
            this.ownersRepository = ownersRepository;
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
    }
}
