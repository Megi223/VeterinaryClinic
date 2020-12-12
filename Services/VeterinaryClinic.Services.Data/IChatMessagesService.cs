using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;

namespace VeterinaryClinic.Services.Data
{
    public interface IChatMessagesService
    {
        Task CreateAsync(RoleName senderRoleName, RoleName receiverRoleName, string ownerId, string vetId, string content);

        IEnumerable<T> GetLatestChatMessages<T>(RoleName receiverRoleName, string currentReceiverUserId);

        Task MarkAsReadAsync(int id);
    }
}
