namespace VeterinaryClinic.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Services.Data.Tests.TestViewModels;
    using VeterinaryClinic.Services.Mapping;
    using Xunit;

    public class NewsServiceTests
    {
        public NewsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(NewsViewModelTest).Assembly);
        }

        [Fact]
        public void GetCountShouldReturnCorrectNumberOfNews()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var newsRepository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));

            var newsService = new NewsService(newsRepository);

            var count = newsService.GetCount();

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectEntity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));
            await repository.AddAsync(new News { Title = "test", Content = "testContent", Summary = "testSummary" });
            await repository.SaveChangesAsync();
            var newsService = new NewsService(repository);
            var news = newsService.GetById<NewsViewModelTest>(1);

            Assert.Equal("test", news.Title);
            Assert.Equal("testContent", news.Content);
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
            var newsService = new NewsService(repository);
            var news = newsService.GetAllForAPage<NewsViewModelTest>(1).ToList();
            Assert.Equal(6, news.Count);
            for (int i = 1; i <= news.Count(); i++)
            {
                Assert.Equal("test" + i, news[i - 1].Title);
                Assert.Equal("testContent" + i, news[i - 1].Content);
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
            var newsService = new NewsService(repository);
            var news = newsService.GetAllForAPage<NewsViewModelTest>(page).ToList();

            var actualCount = news.Count();

            Assert.Equal(expectedCount, actualCount);
        }
    }
}
