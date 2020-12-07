using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinic.Web.ViewModels.Notifications;

namespace VeterinaryClinic.Services.Data
{
    public interface INotificationsService
    {
        Task CreateNotificationForOwnerAsync(string ownerId, string content);

        Task<SendNotificationViewModel> CreateNotificationForVetAsync(string vetId, string content);

        List<T> GetOwnerNotifications<T>(string ownerId);

        List<T> GetVetNotifications<T>(string vetId);

        Task Delete(int id);
    }
}