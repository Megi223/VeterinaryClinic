using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.ViewModels.Pets;

namespace VeterinaryClinic.Services.Data
{
    public class PetsService : IPetsService
    {
        private const int PetsOnOnePage= 3;
        private readonly IDeletableEntityRepository<Pet> petsRepository;

        public PetsService(IDeletableEntityRepository<Pet> petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        //public IEnumerable<T> GetPets<T>(string ownerId)
        //{
        //    return this.petsRepository.AllAsNoTracking().Where(p => p.OwnerId == /ownerId)
        //        .To<T>().ToList();
        //}

        public async Task AddPetAsync(string ownerId,AddPetInputModel model,string photoUrl)
        {
            Pet pet = new Pet
            {
                Name = model.Name,
                Sterilised = bool.Parse(model.Sterilised),
                Birthday = model.Birthday,
                Type = Enum.Parse<TypeOfAnimal>(model.Type),
                Gender = Enum.Parse<Gender>(model.Gender),
                IdentificationNumber = model.IdentificationNumber,
                Picture = photoUrl,
                VetId = model.VetId,
                Weight = model.Weight,
                OwnerId = ownerId,
            };

            await this.petsRepository.AddAsync(pet);
            await this.petsRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.petsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAllForAPage<T>(int page, string ownerId)
        {

            IQueryable<Pet> query =
                this.petsRepository.All().Where(p => p.OwnerId == ownerId)
            .Skip((page - 1) * PetsOnOnePage)
                .Take(PetsOnOnePage);

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var pet = this.petsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return pet;
        }
    }
}
