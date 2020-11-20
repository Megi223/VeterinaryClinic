namespace VeterinaryClinic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;

        public ServicesService(IDeletableEntityRepository<Service> servicesRepository)
        {
            this.servicesRepository = servicesRepository;
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
    }
}
