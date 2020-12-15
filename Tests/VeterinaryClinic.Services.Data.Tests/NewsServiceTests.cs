namespace VeterinaryClinic.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Services.Data.Tests.TestViewModels;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.News;
    using Xunit;

    public class NewsServiceTests
    {
        private readonly ICloudinaryService cloudinaryService;

        public NewsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(NewsViewModelTest).Assembly);
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            this.cloudinaryService = new CloudinaryService(cloudinary);
        }

        [Fact]
        public void GetCountShouldReturnCorrectNumberOfNews()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var newsRepository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));

            var newsService = new NewsService(newsRepository,this.cloudinaryService);

            var count = newsService.GetCount();

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task AddNewsAsyncShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var newsRepository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));

            var newsService = new NewsService(newsRepository,this.cloudinaryService);

            await newsService.AddNewsAsync(new AddNewsInputModel { Content = "testContent", Summary = "testSummary", Title = "testTitle", Image = null });

            Assert.Equal(1, newsRepository.All().Count());
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectEntity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new News { Title = "test", Content = "testContent", Summary = "testSummary" });
            await repository.SaveChangesAsync();

            var newsService = new NewsService(repository,this.cloudinaryService);
            var news = newsService.GetById<NewsViewModelTest>(1);

            Assert.Equal("test", news.Title);
            Assert.Equal("testContent", news.Content);
            Assert.Equal("testSummary", news.Summary);
            Assert.Null(news.ImageUrl);
        }

        [Fact]
        public async Task GetAllForAPageShouldReturnCorrectEntities()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new News { Title = "test1", Content = "testContent1", Summary = "testSummary1" });
            await repository.AddAsync(new News { Title = "test2", Content = "testContent2", Summary = "testSummary2" });
            await repository.AddAsync(new News { Title = "test3", Content = "testContent3", Summary = "testSummary3" });
            await repository.AddAsync(new News { Title = "test4", Content = "testContent4", Summary = "testSummary4" });
            await repository.AddAsync(new News { Title = "test5", Content = "testContent5", Summary = "testSummary5" });
            await repository.AddAsync(new News { Title = "test6", Content = "testContent6", Summary = "testSummary6" });
            await repository.AddAsync(new News { Title = "test7", Content = "testContent7", Summary = "testSummary7" });
            await repository.SaveChangesAsync();

            var newsService = new NewsService(repository,this.cloudinaryService);
            var news = newsService.GetAllForAPage<NewsViewModelTest>(1).ToList();

            for (int i = news.Count(); i <= 1; i++)
            {
                Assert.Equal("test" + i, news[i - 1].Title);
                Assert.Equal("testContent" + i, news[i - 1].Content);
                Assert.Equal("testSummary" + i, news[i - 1].Summary);
                Assert.Null(news[i - 1].ImageUrl);
            }
        }

        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        public async Task GetAllForAPageShouldReturnCorrectCount(int page, int expectedCount)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new News { Title = "test1", Content = "testContent1", Summary = "testSummary1" });
            await repository.AddAsync(new News { Title = "test2", Content = "testContent2", Summary = "testSummary2" });
            await repository.AddAsync(new News { Title = "test3", Content = "testContent3", Summary = "testSummary3" });
            await repository.AddAsync(new News { Title = "test4", Content = "testContent4", Summary = "testSummary4" });
            await repository.AddAsync(new News { Title = "test5", Content = "testContent5", Summary = "testSummary5" });
            await repository.AddAsync(new News { Title = "test6", Content = "testContent6", Summary = "testSummary6" });
            await repository.AddAsync(new News { Title = "test7", Content = "testContent7", Summary = "testSummary7" });
            await repository.SaveChangesAsync();

            var newsService = new NewsService(repository,this.cloudinaryService);
            var news = newsService.GetAllForAPage<NewsViewModelTest>(page).ToList();

            var actualCount = news.Count();

            Assert.Equal(expectedCount, actualCount);
        }
    }
}
