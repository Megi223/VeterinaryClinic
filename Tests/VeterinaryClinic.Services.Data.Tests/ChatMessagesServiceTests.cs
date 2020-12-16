namespace VeterinaryClinic.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Services.Data.Tests.TestViewModels;
    using VeterinaryClinic.Services.Mapping;
    using Xunit;

    public class ChatMessagesServiceTests
    {
        [Fact]
        public async Task CreateAsyncShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var chatMessageRepository = new EfDeletableEntityRepository<ChatMessage>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));
            var ownersRepository = new EfDeletableEntityRepository<Owner>(new ApplicationDbContext(options.Options));

            var service = new ChatMessagesService(chatMessageRepository, ownersRepository, vetsRepository);

            await service.CreateAsync(RoleName.Vet, RoleName.Owner, "testOwnerId", "testVetId", "testContent");

            var count = chatMessageRepository.All().Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public void GetLatestChatMessagesShouldReturnCorrectCountOfEntitiesWhenReceiverIsOwner()
        {
            var chatMessagesRepository = new Mock<IDeletableEntityRepository<ChatMessage>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var ownersRepository = new Mock<IDeletableEntityRepository<Owner>>();

            chatMessagesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForChatMessagesRepository().AsQueryable());

            ownersRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForOwnersRepository().AsQueryable());

            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetsRepository().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(ChatMessageViewModelTest).Assembly);

            IChatMessagesService service = new ChatMessagesService(chatMessagesRepository.Object, ownersRepository.Object, vetsRepository.Object);

            var actualLatestMessages = service.GetLatestChatMessages<ChatMessageViewModelTest>(RoleName.Owner, "testUserIdForOwner1").ToList();

            Assert.Single(actualLatestMessages);
        }

        [Fact]
        public void GetLatestChatMessagesShouldReturnCorrectEntitiesWhenReceiverIsOwner()
        {
            var chatMessagesRepository = new Mock<IDeletableEntityRepository<ChatMessage>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var ownersRepository = new Mock<IDeletableEntityRepository<Owner>>();

            chatMessagesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForChatMessagesRepository().AsQueryable());

            ownersRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForOwnersRepository().AsQueryable());

            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetsRepository().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(ChatMessageViewModelTest).Assembly);

            IChatMessagesService service = new ChatMessagesService(chatMessagesRepository.Object, ownersRepository.Object, vetsRepository.Object);

            var actualLatestMessages = service.GetLatestChatMessages<ChatMessageViewModelTest>(RoleName.Owner, "testUserIdForOwner1").ToList();

            for (int i = 1; i <= actualLatestMessages.Count(); i++)
            {
                Assert.Equal(1, actualLatestMessages[i - 1].Id);
                Assert.Equal(RoleName.Owner, actualLatestMessages[i - 1].ReceiverRole);
                Assert.Equal(RoleName.Vet, actualLatestMessages[i - 1].SenderRole);
                Assert.Equal("testOwnerId1", actualLatestMessages[i - 1].OwnerId);
                Assert.Equal("testVetId1", actualLatestMessages[i - 1].VetId);
                Assert.Equal("testContent1", actualLatestMessages[i - 1].Content);
                Assert.False(actualLatestMessages[i - 1].IsRead);
            }
        }

        [Fact]
        public void GetLatestChatMessagesShouldReturnCorrectCountOfEntitiesWhenReceiverIsVet()
        {
            var chatMessagesRepository = new Mock<IDeletableEntityRepository<ChatMessage>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var ownersRepository = new Mock<IDeletableEntityRepository<Owner>>();

            chatMessagesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForChatMessagesRepository().AsQueryable());

            ownersRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForOwnersRepository().AsQueryable());

            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetsRepository().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(ChatMessageViewModelTest).Assembly);

            IChatMessagesService service = new ChatMessagesService(chatMessagesRepository.Object, ownersRepository.Object, vetsRepository.Object);

            var actualLatestMessages = service.GetLatestChatMessages<ChatMessageViewModelTest>(RoleName.Vet, "testUserIdForVet2").ToList();

            Assert.Single(actualLatestMessages);
        }

        [Fact]
        public void GetLatestChatMessagesShouldReturnCorrectEntitiesWhenReceiverIsVet()
        {
            var chatMessagesRepository = new Mock<IDeletableEntityRepository<ChatMessage>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var ownersRepository = new Mock<IDeletableEntityRepository<Owner>>();

            chatMessagesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForChatMessagesRepository().AsQueryable());

            ownersRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForOwnersRepository().AsQueryable());

            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetsRepository().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(ChatMessageViewModelTest).Assembly);

            IChatMessagesService service = new ChatMessagesService(chatMessagesRepository.Object, ownersRepository.Object, vetsRepository.Object);

            var actualLatestMessages = service.GetLatestChatMessages<ChatMessageViewModelTest>(RoleName.Vet, "testUserIdForVet2").ToList();

            for (int i = 1; i <= actualLatestMessages.Count(); i++)
            {
                Assert.Equal(2, actualLatestMessages[i - 1].Id);
                Assert.Equal(RoleName.Owner, actualLatestMessages[i - 1].SenderRole);
                Assert.Equal(RoleName.Vet, actualLatestMessages[i - 1].ReceiverRole);
                Assert.Equal("testOwnerId2", actualLatestMessages[i - 1].OwnerId);
                Assert.Equal("testVetId2", actualLatestMessages[i - 1].VetId);
                Assert.Equal("testContent2", actualLatestMessages[i - 1].Content);
                Assert.False(actualLatestMessages[i - 1].IsRead);
            }
        }

        [Fact]
        public async Task MarkAsReadAsyncShouldSetIsReadToTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var chatMessageRepository = new EfDeletableEntityRepository<ChatMessage>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));
            var ownersRepository = new EfDeletableEntityRepository<Owner>(new ApplicationDbContext(options.Options));

            var service = new ChatMessagesService(chatMessageRepository, ownersRepository, vetsRepository);

            await chatMessageRepository.AddAsync(
                new ChatMessage
                {
                    Id = 1,
                    ReceiverRole = RoleName.Owner,
                    SenderRole = RoleName.Vet,
                    OwnerId = "testOwnerId1",
                    VetId = "testVetId1",
                    Content = "testContent1",
                    IsRead = false,
                });
            await chatMessageRepository.SaveChangesAsync();

            await service.MarkAsReadAsync(1);

            Assert.True(chatMessageRepository.All().First().IsRead);
        }

        private List<Owner> GetTestDataForOwnersRepository()
        {
            return new List<Owner>
            {
                new Owner
                {
                    Id = "testOwnerId1",
                    FirstName = "testFirstName1",
                    LastName = "testLastName1",
                    UserId = "testUserIdForOwner1",
                },
                new Owner
                {
                    Id = "testOwnerId2",
                    FirstName = "testFirstName2",
                    LastName = "testLastName2",
                    UserId = "testUserIdForOwner2",
                },
            };
        }

        private List<Vet> GetTestDataForVetsRepository()
        {
            return new List<Vet>
            {
                new Vet
                {
                    Id = "testVetId1",
                    FirstName = "testFirstName1",
                    LastName = "testLastName1",
                    UserId = "testUserIdForVet1",
                },
                new Vet
                {
                    Id = "testVetId2",
                    FirstName = "testFirstName2",
                    LastName = "testLastName2",
                    UserId = "testUserIdForVet2",
                },
            };
        }

        private List<ChatMessage> GetTestDataForChatMessagesRepository()
        {
            return new List<ChatMessage>
            {
                new ChatMessage
                {
                    Id = 1,
                    ReceiverRole = RoleName.Owner,
                    SenderRole = RoleName.Vet,
                    OwnerId = "testOwnerId1",
                    VetId = "testVetId1",
                    Content = "testContent1",
                    IsRead = false,
                },
                new ChatMessage
                {
                    Id = 2,
                    ReceiverRole = RoleName.Vet,
                    SenderRole = RoleName.Owner,
                    OwnerId = "testOwnerId2",
                    VetId = "testVetId2",
                    Content = "testContent2",
                    IsRead = false,
                },
            };
        }
    }
}
