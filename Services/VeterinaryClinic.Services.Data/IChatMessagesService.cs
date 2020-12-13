namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Models.Enumerations;

    public interface IChatMessagesService
    {
        Task CreateAsync(RoleName senderRoleName, RoleName receiverRoleName, string ownerId, string vetId, string content);

        IEnumerable<T> GetLatestChatMessages<T>(RoleName receiverRoleName, string currentReceiverUserId);

        Task MarkAsReadAsync(int id);
    }
}
