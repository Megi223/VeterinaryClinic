using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.ViewModels.Gallery;
using Xunit;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class GalleryServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumberOfNews()
        {
            var repository = new Mock<IDeletableEntityRepository<Gallery>>();

            repository.Setup(r => r.AllAsNoTracking())

            .Returns(this.GetTestData().AsQueryable());

            IGalleryService service = new GalleryService(repository.Object);

            var actualCount = service.GetCount();

            Assert.Equal(7, actualCount);
        }

        [Fact]
        public void GetAllForAPageShouldReturnCorrectEntities()
        {
            var repository = new Mock<IDeletableEntityRepository<Gallery>>();

            repository.Setup(r => r.AllAsNoTracking())

            .Returns(this.GetTestData().AsQueryable());
            AutoMapperConfig.RegisterMappings(typeof(GalleryAllViewModel).Assembly);
            IGalleryService service = new GalleryService(repository.Object);

            List<GalleryAllViewModel> gallery = service.GetAllForAPage<GalleryAllViewModel>(1).ToList();

            for (int i = 1; i <= gallery.Count(); i++)
            {
                Assert.Equal("test" + i, gallery[i - 1].Title);
                Assert.Null(gallery[i - 1].ImageUrl);
            }

        }

        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        public async Task GetAllForAPageShouldReturnCorrectCount(int page, int expectedCount)
        {
            var repository = new Mock<IDeletableEntityRepository<Gallery>>();

            repository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            AutoMapperConfig.RegisterMappings(typeof(GalleryAllViewModel).Assembly);
            IGalleryService service = new GalleryService(repository.Object);

            List<GalleryAllViewModel> gallery = service.GetAllForAPage<GalleryAllViewModel>(page).ToList();
            var actualCount = gallery.Count();

            Assert.Equal(expectedCount, actualCount);

        }

        private List<Gallery> GetTestData()
        {
            return new List<Gallery>()
            {
                new Gallery { Title = "test1" },
                new Gallery { Title = "test2" },
                new Gallery { Title = "test3" },
                new Gallery { Title = "test4" },
                new Gallery { Title = "test5" },
                new Gallery { Title = "test6" },
                new Gallery { Title = "test7" },
            };
        }
    }
}
