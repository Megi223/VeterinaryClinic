using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;

namespace VeterinaryClinic.Services.Data
{
    public class ChatMessagesService : IChatMessagesService
    {
        private readonly IDeletableEntityRepository<ChatMessage> chatMessagesRepository;

        public ChatMessagesService(IDeletableEntityRepository<ChatMessage> chatMessagesRepository)
        {
            this.chatMessagesRepository = chatMessagesRepository;
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

    }
}
