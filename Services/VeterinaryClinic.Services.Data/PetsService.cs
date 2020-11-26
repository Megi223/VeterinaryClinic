using Microsoft.AspNetCore.Http;
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
        private const string DogDefaultPhotoUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324362/dog_gxboja.png";
        private const string CatDefaultPhotoUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324384/cat_y8yj67.png";
        private const string RabbitDefaultPhotoUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324403/rabbit_mfqxtl.png";
        private const string TurtleDefaultPhotoUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324426/turtle_xhvrsx.png";
        private const string HamsterDefaultPhotoUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324447/hamster_ex7aso.png";
        private const string ParrotDefaultPhotoUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324468/parrot_ajr3j3.png";

        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly ICloudinaryService cloudinaryService;
        

        public PetsService(IDeletableEntityRepository<Pet> petsRepository,ICloudinaryService cloudinaryService)
        {
            this.petsRepository = petsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<string> DeterminePhotoUrl(IFormFile inputImage,string typeOfAnimal)
        {
            string photoUrl = string.Empty;
            if (inputImage != null)
            {
                photoUrl = await this.cloudinaryService.UploudAsync(inputImage);
            }
            else
            {
                switch (typeOfAnimal)
                {
                    case "1": photoUrl = DogDefaultPhotoUrl; break;
                    case "2": photoUrl = CatDefaultPhotoUrl; break;
                    case "3": photoUrl = RabbitDefaultPhotoUrl; break;
                    case "4": photoUrl = TurtleDefaultPhotoUrl; break;
                    case "5": photoUrl = ParrotDefaultPhotoUrl; break;
                    case "6": photoUrl = HamsterDefaultPhotoUrl; break;
                }
            }
            return photoUrl;
        }

        public async Task AddPetAsync(string ownerId,AddPetInputModel model,string photoUrl)
        {
            bool isIdentificationNumberValid = this.IsIdentificationNumberValid(model.IdentificationNumber);
            if (!isIdentificationNumberValid)
            {
                throw new ArgumentException("Invalid identification number!");
            }
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

        private bool IsIdentificationNumberValid(string identificationNumber)
        {
            var existingIdNumber = this.petsRepository.AllAsNoTracking().FirstOrDefault(p => p.IdentificationNumber == identificationNumber);

            if (existingIdNumber != null)
            {
                return false;
            }

            return true;
        }
    }
}
