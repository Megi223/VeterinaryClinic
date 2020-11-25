using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data
{
    public class VetsService : IVetsService
    {
        private readonly IDeletableEntityRepository<Vet> vetsRepository;

        public VetsService(IDeletableEntityRepository<Vet> vetsRepository)
        {
            this.vetsRepository = vetsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Vet> query =
                this.vetsRepository.All().OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

            return query.To<T>().ToList();
        }
    }
}
