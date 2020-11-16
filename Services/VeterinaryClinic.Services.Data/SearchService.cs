using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace VeterinaryClinic.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Vet> vetsRepository;
        private readonly IDeletableEntityRepository<Service> servicesRepository;
        private readonly IDeletableEntityRepository<News> newsRepository;

        public SearchService(IDeletableEntityRepository<Vet> vetsRepository, IDeletableEntityRepository<Service> servicesRepository, IDeletableEntityRepository<News> newsRepository)
        {
            this.vetsRepository = vetsRepository;
            this.servicesRepository = servicesRepository;
            this.newsRepository = newsRepository;
        }

        public List<string> SearchVet(string term)
        {
            return this.vetsRepository.AllAsNoTracking().Where(v => v.FirstName.ToLower().Contains(term) || v.LastName.ToLower().Contains(term))
                .Select(v => v.FirstName + " " + v.LastName).ToList();
        }

        public List<string> SearchServices(string term)
        {
            return this.servicesRepository.AllAsNoTracking().Where(s => s.Name.ToLower().Contains(term))
                .Select(s => s.Name).ToList();
        }

        public List<string> SearchNews(string term)
        {
            return this.newsRepository.AllAsNoTracking().Where(n => n.Title.ToLower().Contains(term) || n.Summary.ToLower().Contains(term))
                .Select(n => n.Title).ToList();
        }
    }
}
