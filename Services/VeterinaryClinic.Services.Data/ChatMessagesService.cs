namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Services.Mapping;

    public class ChatMessagesService : IChatMessagesService
    {
        private readonly IDeletableEntityRepository<ChatMessage> chatMessagesRepository;
        private readonly IDeletableEntityRepository<Owner> ownersRepository;
        private readonly IDeletableEntityRepository<Vet> vetsRepository;

        public ChatMessagesService(IDeletableEntityRepository<ChatMessage> chatMessagesRepository, IDeletableEntityRepository<Owner> ownersRepository, IDeletableEntityRepository<Vet> vetsRepository)
        {
            this.chatMessagesRepository = chatMessagesRepository;
            this.ownersRepository = ownersRepository;
            this.vetsRepository = vetsRepository;
        }

        public async Task CreateAsync(RoleName senderRoleName, RoleName receiverRoleName, string ownerId, string vetId, string content)
        {
            ChatMessage chatMessage = new ChatMessage
            {
                SenderRole = senderRoleName,
                ReceiverRole = receiverRoleName,
                OwnerId = ownerId,
                VetId = vetId,
                Content = content,
            };

            await this.chatMessagesRepository.AddAsync(chatMessage);
            await this.chatMessagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetLatestChatMessages<T>(RoleName receiverRoleName, string currentReceiverUserId)
        {
            if (receiverRoleName == RoleName.Owner)
            {
                string ownerId = this.ownersRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == currentReceiverUserId).Id;
                var model = this.chatMessagesRepository.All().Where(x => x.OwnerId == ownerId && x.ReceiverRole == RoleName.Owner && x.IsRead == false);
                return model.To<T>().ToList();
            }

            string vetId = this.vetsRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == currentReceiverUserId).Id;
            var viewModel = this.chatMessagesRepository.All().Where(x => x.VetId == vetId && x.ReceiverRole == RoleName.Vet && x.IsRead == false);
            return viewModel.To<T>().ToList();
        }

        public async Task MarkAsReadAsync(int id)
        {
            this.chatMessagesRepository.All().FirstOrDefault(x => x.Id == id).IsRead = true;
            await this.chatMessagesRepository.SaveChangesAsync();
        }
    }
}
