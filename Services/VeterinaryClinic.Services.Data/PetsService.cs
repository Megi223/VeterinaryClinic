using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data
{
    public class PetsService : IPetsService
    {
        private readonly IDeletableEntityRepository<Pet> petsRepository;

        public PetsService(IDeletableEntityRepository<Pet> petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        public IEnumerable<T> GetPets<T>(string userId)
        {
            return this.petsRepository.AllAsNoTracking().Where(p => p.OwnerId == userId)
                .To<T>().ToList();
        }
    }
}
