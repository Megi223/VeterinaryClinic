namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VeterinaryClinic.Web.ViewModels.Notifications;

    public interface INotificationsService
    {
        Task CreateNotificationForOwnerAsync(string ownerId, string content);

        Task<SendNotificationViewModel> CreateNotificationForVetAsync(string vetId, string content);

        List<T> GetOwnerNotifications<T>(string ownerId);

        List<T> GetVetNotifications<T>(string vetId);

        Task Delete(int id);
    }
}