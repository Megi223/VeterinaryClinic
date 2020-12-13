namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.Vets;

    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;
        private readonly IDeletableEntityRepository<VetsServices> vetsServicesRepository;

        public ServicesService(IDeletableEntityRepository<Service> servicesRepository, IDeletableEntityRepository<VetsServices> vetsServicesRepository)
        {
            this.servicesRepository = servicesRepository;
            this.vetsServicesRepository = vetsServicesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Service> query =
                this.servicesRepository.All();

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var service = this.servicesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return service;
        }

        public string GetNameById(int id)
        {
            var service = this.servicesRepository.All().Where(x => x.Id == id)
                .FirstOrDefault();
            return service.Name;
        }

        public IEnumerable<T> GetAllServicesWhichAVetDoesNotHave<T>(string vetId)
        {
            var vetsServices =
                this.vetsServicesRepository.All().Where(x => x.VetId == vetId);
            var services = this.servicesRepository.All();
            foreach (var vetsService in vetsServices)
            {
                services = services.Where(x => x.Id.ToString() != vetsService.ServiceId);
            }

            return services.To<T>().ToList();
        }

        public async Task AddServiceToVet(AddServiceToVetInputModel input)
        {
            foreach (var serviceId in input.Services)
            {
                var service = this.servicesRepository.All().FirstOrDefault(x => x.Id == serviceId);
                VetsServices vetsServices = new VetsServices
                {
                    VetId = input.VetId,
                    ServiceId = serviceId.ToString(),
                    Service = service,
                };

                await this.vetsServicesRepository.AddAsync(vetsServices);
            }

            await this.vetsServicesRepository.SaveChangesAsync();
        }
    }
}
