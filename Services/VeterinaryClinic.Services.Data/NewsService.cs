namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public IEnumerable<T> GetAllForAPage<T>(int page)
        {
            IQueryable<News> query =
                this.newsRepository.All()
            .Skip((page - 1) * GlobalConstants.NewsOnOnePage)
                .Take(GlobalConstants.NewsOnOnePage);

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var news = this.newsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return news;
        }

        public int GetCount()
        {
            return this.newsRepository.AllAsNoTracking().Count();
        }
    }
}
