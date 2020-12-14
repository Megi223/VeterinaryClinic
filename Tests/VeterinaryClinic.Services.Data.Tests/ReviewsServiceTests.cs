using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Data.Tests.TestViewModels;
using VeterinaryClinic.Services.Mapping;
using Xunit;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class ReviewsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumberOfReviews()
        {
            var repository = new Mock<IDeletableEntityRepository<Review>>();
            repository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestData().AsQueryable());

            IReviewsService service = new ReviewsService(repository.Object);

            var actualCount = service.GetCount();

            Assert.Equal(7, actualCount);
        }

        [Fact]
        public void GetAllForAPageShouldReturnCorrectEntities()
        {
            var repository = new Mock<IDeletableEntityRepository<Review>>();
            repository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());

            IReviewsService service = new ReviewsService(repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(ReviewViewModelTest).Assembly);

            List<ReviewViewModelTest> actualReviews = service.GetAllForAPage<ReviewViewModelTest>(1).ToList();

            for (int i = 1; i <= actualReviews.Count(); i++)
            {
                Assert.Equal(i, actualReviews[i - 1].Id);
                Assert.Equal("testContent" + i, actualReviews[i - 1].Content);
                Assert.Equal("testOwnerId" + i, actualReviews[i - 1].OwnerId);
            }
        }

        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        public void GetAllForAPageShouldReturnCorrectCount(int page, int expectedCount)
        {
            var repository = new Mock<IDeletableEntityRepository<Review>>();
            repository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());

            IReviewsService service = new ReviewsService(repository.Object);
            AutoMapperConfig.RegisterMappings(typeof(ReviewViewModelTest).Assembly);

            List<ReviewViewModelTest> actualReviews = service.GetAllForAPage<ReviewViewModelTest>(page).ToList();

            var actualCount = actualReviews.Count();

            Assert.Equal(expectedCount, actualCount);
        }

        private List<Review> GetTestData()
        {
            return new List<Review>()
            {
                new Review { Id = 1, Content = "testContent1", OwnerId = "testOwnerId1" },
                new Review { Id = 2, Content = "testContent2", OwnerId = "testOwnerId2" },
                new Review { Id = 3, Content = "testContent3", OwnerId = "testOwnerId3" },
                new Review { Id = 4, Content = "testContent4", OwnerId = "testOwnerId4" },
                new Review { Id = 5, Content = "testContent5", OwnerId = "testOwnerId5" },
                new Review { Id = 6, Content = "testContent6", OwnerId = "testOwnerId6" },
                new Review { Id = 7, Content = "testContent7", OwnerId = "testOwnerId7" },
            };
        }
    }
}
