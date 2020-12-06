namespace VeterinaryClinic.Services.Data
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.Vets;

    public class VetsService : IVetsService
    {
        private const string DefaultImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606144918/default-profile-icon-16_vbh95n.png";

        private readonly IDeletableEntityRepository<Vet> vetsRepository;
        private readonly IDeletableEntityRepository<Service> servicesRepository;
        private readonly IDeletableEntityRepository<VetsServices> vetsServicesRepository;
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly ICloudinaryService cloudinaryService;

        private readonly IServicesService servicesService;

        public VetsService(IDeletableEntityRepository<Vet> vetsRepository, IDeletableEntityRepository<VetsServices> vetsServicesRepository, IServicesService servicesService, ICloudinaryService cloudinaryService, IDeletableEntityRepository<Pet> petsRepository, IDeletableEntityRepository<Service> servicesRepository)
        {
            this.vetsRepository = vetsRepository;
            this.vetsServicesRepository = vetsServicesRepository;
            this.servicesService = servicesService;
            this.cloudinaryService = cloudinaryService;
            this.petsRepository = petsRepository;
            this.servicesRepository = servicesRepository;
        }

        public IEnumerable<T> GetAllForAPage<T>(int page)
        {
            IQueryable<Vet> query =
                this.vetsRepository.All()
            .Skip((page - 1) * GlobalConstants.VetsOnOnePage)
                .Take(GlobalConstants.VetsOnOnePage);

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var vet = this.vetsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return vet;
        }

        public int GetCount()
        {
            return this.vetsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Vet> query =
                this.vetsRepository.All().OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

            return query.To<T>().ToList();
        }

        public string GetServices(string vetId)
        {
            var serviceNames = new List<string>();
            var serviceIds = this.vetsServicesRepository.All().Where(vs => vs.VetId == vetId).Select(vs => vs.ServiceId).ToList();
            foreach (var serviceId in serviceIds)
            {
                string serviceName = this.servicesService.GetNameById(int.Parse(serviceId));
                serviceNames.Add(serviceName);
            }

            return string.Join(", ", serviceNames);
        }

        public string GetVetId(string userId)
        {
            return this.vetsRepository.AllAsNoTracking().Where(x => x.UserId == userId).FirstOrDefault().Id;
        }

        public async Task<string> DeterminePhotoUrl(IFormFile input)
        {
            string photoUrl = string.Empty;
            if (input != null)
            {
                photoUrl = await this.cloudinaryService.UploudAsync(input);
            }
            else
            {
                photoUrl = DefaultImageUrl;
            }

            return photoUrl;
        }

        public async Task AddVetAsync(string userId, AddVetInputModel model, string photoUrl)
        {
            Vet vet = new Vet
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfilePicture = photoUrl,
                HireDate = model.HireDate,
                Specialization = model.Specialization,
                UserId = userId,
            };

            await this.vetsRepository.AddAsync(vet);
            await this.vetsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetVetsPatientsForAPage<T>(string vetId, int page)
        {
            return this.petsRepository.AllAsNoTracking().Where(x => x.VetId == vetId)
                .OrderBy(x => x.Name).Skip((page - 1) * GlobalConstants.VetsPatientsOnOnePage)
                .Take(GlobalConstants.VetsPatientsOnOnePage).To<T>().ToList();
        }

        public int GetPatientsCount(string vetId)
        {
            return this.petsRepository.AllAsNoTracking().Where(x => x.VetId == vetId)
                .ToList().Count;
        }

        public string GetNameById(string vetId)
        {
            var vet = this.vetsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == vetId);
            return vet.FirstName + " " + vet.LastName;
        }

        public async Task DeleteVet(string vetId)
        {
            var vet = this.vetsRepository.All().FirstOrDefault(x => x.Id == vetId);
            this.vetsRepository.Delete(vet);
            await this.vetsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetServices<T>(string vetId)
        {
            var vetsServices = this.vetsServicesRepository.All().Where(vs => vs.VetId == vetId);
            return vetsServices.To<T>().ToList();
        }

        public async Task EditVet(EditVetInputModel input)
        {
            var vet = this.vetsRepository.All().FirstOrDefault(x => x.Id == input.Id);
            if (input.Picture != null)
            {
                vet.ProfilePicture = await this.cloudinaryService.UploudAsync(input.Picture);
            }

            vet.Specialization = input.Specialization;
            if(input.ServicesInput == null)
            {
                await this.DeleteAllVetsServices(input.Id);
            }
            else if (input.ServicesInput != null && input.ServicesInput.Count() != vet.VetsServices.Count)
            {
                await this.DeleteAllVetsServices(input.Id);
                await this.AddServicesToVet(input.ServicesInput,input.Id);
            }

            await this.vetsRepository.SaveChangesAsync();
        }

        private async Task DeleteAllVetsServices(string vetId)
        {
            var vetsServices = this.vetsServicesRepository.All().Where(x => x.VetId == vetId);

            foreach (var vetsService in vetsServices)
            {
                this.vetsServicesRepository.Delete(vetsService);
            }

            await this.vetsServicesRepository.SaveChangesAsync();
        }

        private async Task AddServicesToVet(IEnumerable<string> serviceIds,string vetId)
        {
            foreach (var serviceId in serviceIds)
            {
                var service = this.servicesRepository.All().FirstOrDefault(x => x.Id.ToString() == serviceId);
                VetsServices vetsServices = new VetsServices
                {
                    VetId = vetId,
                    ServiceId = serviceId.ToString(),
                    Service = service,
                };

                await this.vetsServicesRepository.AddAsync(vetsServices);
            }

            await this.vetsServicesRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetVetsPatients<T>(string vetId)
        {
            return this.petsRepository.All().Where(x => x.VetId == vetId).To<T>();
        }
    }
}
