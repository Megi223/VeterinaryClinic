using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using Xunit;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class UsersServiceTests
    {
        [Theory]
        [InlineData("testId1", "testUserName1")]
        [InlineData("testId2", "testUserName2")]
        public void GetUserUserNameShouldReturnCorrectUserName(string userId, string expectedUserName)
        {
            var usersRepository = new Mock<IRepository<ApplicationUser>>();
            usersRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestData().AsQueryable());

            IUsersService usersService = new UsersService(usersRepository.Object);

            var actualUserName = usersService.GetUserUserName(userId);

            Assert.Equal(expectedUserName, actualUserName);
        }

        private List<ApplicationUser> GetTestData()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "testId1",
                    UserName = "testUserName1",
                },
                new ApplicationUser
                {
                    Id = "testId2",
                    UserName = "testUserName2",
                },
            };
        }
    }
}
