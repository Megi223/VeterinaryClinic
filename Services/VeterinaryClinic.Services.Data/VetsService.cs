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
    public class VetsService : IVetsService
    {
        private readonly IDeletableEntityRepository<Vet> vetsRepository;
        private readonly IDeletableEntityRepository<VetsServices> vetsServicesRepository;

        private readonly IServicesService servicesService;

        public VetsService(IDeletableEntityRepository<Vet> vetsRepository, IDeletableEntityRepository<VetsServices> vetsServicesRepository, IServicesService servicesService)
        {
            this.vetsRepository = vetsRepository;
            this.vetsServicesRepository = vetsServicesRepository;
            this.servicesService = servicesService;
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
            var vets = this.vetsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return vets;
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
    }
}
