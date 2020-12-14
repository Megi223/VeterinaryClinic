using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;
using VeterinaryClinic.Services.Data.Tests.TestViewModels;
using VeterinaryClinic.Services.Mapping;
using Xunit;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class SearchServiceTests
    {
        public SearchServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(VetViewModelTest).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(NewsViewModelTest).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(ServiceViewModelTest).Assembly);
        }

        [Fact]
        public void SearchVetGenericShouldReturnCorrectEntities()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object,servicesRepository.Object,newsRepository.Object);

            var actualVetsFound = service.SearchVet<VetViewModelTest>("anoth").ToList();

            // This way I check that in the result there are such entities (the expected ones)
            Assert.NotNull(actualVetsFound.Where(x => x.Id == "VetId3").FirstOrDefault());
            Assert.NotNull(actualVetsFound.Where(x => x.Id == "VetId4").FirstOrDefault());
        }

        [Theory]
        [InlineData("Name", 4)]
        [InlineData("1", 1)]
        [InlineData("last", 3)]
        public void SearchVetGenericShouldReturnCorrectCount(string searchTerm, int expectedCount)
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualVetsFoundCount = service.SearchVet<VetViewModelTest>(searchTerm).Count();

            Assert.Equal(expectedCount, actualVetsFoundCount);
        }

        [Fact]
        public void SearchVetListOfStringShouldReturnCorrectList()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualVetsFound = service.SearchVet("anoth");

            var extectedList = new List<string> { "anotherFirstName testLastName3", "testFirstName4 anotherLastName" };

            Assert.Equal(extectedList, actualVetsFound);
        }

        [Fact]
        public void SearchVetListOfStringShouldReturnCorrectCount()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualVetsFoundCount = service.SearchVet("anoth").Count();

            Assert.Equal(2, actualVetsFoundCount);
        }

        [Fact]
        public void SearchServicesGenericShouldReturnCorrectEntities()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            servicesRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForServicesRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualServicesFound = service.SearchServices<ServiceViewModelTest>("est").ToList();

            // This way I check that in the result there are such entities (the expected ones)
            Assert.NotNull(actualServicesFound.Where(x => x.Name == "testName1").FirstOrDefault());
            Assert.NotNull(actualServicesFound.Where(x => x.Name == "testName2").FirstOrDefault());
        }

        [Theory]
        [InlineData("Name", 2)]
        [InlineData("t", 3)]
        [InlineData("zr", 0)]
        public void SearchServiceGenericShouldReturnCorrectCount(string searchTerm, int expectedCount)
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            servicesRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForServicesRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualServicesFoundCount = service.SearchServices<ServiceViewModelTest>(searchTerm).Count();

            Assert.Equal(expectedCount, actualServicesFoundCount);
        }

        [Fact]
        public void SearchServicesListOfStringShouldReturnCorrectList()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            servicesRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForServicesRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualServicesFound = service.SearchServices("thir");

            var extectedList = new List<string> { "third" };

            Assert.Equal(extectedList, actualServicesFound);
        }

        [Fact]
        public void SearchServicesListOfStringShouldReturnCorrectCount()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            servicesRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForServicesRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualServicesFoundCount = service.SearchServices("thir").Count();

            Assert.Equal(1, actualServicesFoundCount);
        }

        [Fact]
        public void SearchNewsGenericShouldReturnCorrectEntities()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            newsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNewsRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualNewsFound = service.SearchNews<NewsViewModelTest>("Titl").ToList();

            // This way I check that in the result there are such entities (the expected ones)
            Assert.NotNull(actualNewsFound.Where(x => x.Title == "someTitle").FirstOrDefault());
            Assert.NotNull(actualNewsFound.Where(x => x.Title == "ThirdTitle").FirstOrDefault());
        }

        [Theory]
        [InlineData("sec", 1)]
        [InlineData("sum", 3)]
        [InlineData("oy", 0)]
        public void SearchNewsGenericShouldReturnCorrectCount(string searchTerm, int expectedCount)
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            newsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNewsRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualNewsFoundCount = service.SearchNews<NewsViewModelTest>(searchTerm).Count();

            Assert.Equal(expectedCount, actualNewsFoundCount);
        }

        [Fact]
        public void SearchNewsListOfStringShouldReturnCorrectList()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            newsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNewsRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualNewsFound = service.SearchNews("itl");

            var extectedList = new List<string> { "someTitle", "ThirdTitle" };

            Assert.Equal(extectedList, actualNewsFound);
        }

        [Fact]
        public void SearchNewsListOfStringShouldReturnCorrectCount()
        {
            var newsRepository = new Mock<IDeletableEntityRepository<News>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            newsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNewsRepository().AsQueryable());

            ISearchService service = new SearchService(vetsRepository.Object, servicesRepository.Object, newsRepository.Object);

            var actualServicesFoundCount = service.SearchNews("tle").Count();

            Assert.Equal(2, actualServicesFoundCount);
        }

        private List<News> GetTestDataForNewsRepository()
        {
            return new List<News>
            {
                new News
                {
                    Title = "someTitle",
                    ImageUrl = null,
                    CreatedOn = new DateTime(2020, 12, 14),
                    Content = "content1",
                    Summary = "testSummary",
                },
                new News
                {
                    Title = "second",
                    ImageUrl = null,
                    CreatedOn = new DateTime(2020, 12, 14),
                    Content = "content1",
                    Summary = "anotherTestSummary",
                },
                new News
                {
                    Title = "ThirdTitle",
                    ImageUrl = null,
                    CreatedOn = new DateTime(2020, 12, 14),
                    Content = "contentTest",
                    Summary = "sum",
                },
            };
        }

        private List<Vet> GetTestDataForVetRepository()
        {
            return new List<Vet>
            {
                new Vet
                {
                    Id = "VetId1",
                    FirstName = "someFirstName",
                    LastName = "testName1",
                    Specialization = "testSpecialization1",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId1",
                    ProfilePicture = null,
                },
                new Vet
                {
                    Id = "VetId2",
                    FirstName = "testFirstName2",
                    LastName = "lastName",
                    Specialization = "testSpecialization2",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId2",
                    ProfilePicture = null,
                },
                new Vet
                {
                    Id = "VetId3",
                    FirstName = "anotherFirstName",
                    LastName = "testLastName3",
                    Specialization = "testSpecialization3",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId3",
                    ProfilePicture = null,
                },
                new Vet
                {
                    Id = "VetId4",
                    FirstName = "testFirstName4",
                    LastName = "anotherLastName",
                    Specialization = "testSpecialization4",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId4",
                    ProfilePicture = null,
                },
            };
        }

        private List<Service> GetTestDataForServicesRepository()
        {
            return new List<Service>()
            {
                new Service
                {
                    Id = 1,
                    Name = "testName1",
                },
                new Service
                {
                    Id = 2,
                    Name = "testName2",
                },
                new Service
                {
                    Id = 1,
                    Name = "third",
                },
            };
        }
    }
}
