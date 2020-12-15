namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.News;

    public class NewsService : INewsService
    {
        private const string DefaultNewsImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1608029941/default-news-image_tg9wfo.png";
        private readonly IDeletableEntityRepository<News> newsRepository;
        private ICloudinaryService cloudinaryService;

        public NewsService(IDeletableEntityRepository<News> newsRepository, ICloudinaryService cloudinaryService)
        {
            this.newsRepository = newsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public IEnumerable<T> GetAllForAPage<T>(int page)
        {
            IQueryable<News> query =
                this.newsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
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

        public async Task AddNewsAsync(AddNewsInputModel model)
        {
            News news = new News
            {
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                ImageUrl = model.Image == null ? DefaultNewsImageUrl : await this.cloudinaryService.UploudAsync(model.Image),
            };

            await this.newsRepository.AddAsync(news);
            await this.newsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetLatestNews<T>()
        {
            return this.newsRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().Take(2).ToList();
        }
    }
}
