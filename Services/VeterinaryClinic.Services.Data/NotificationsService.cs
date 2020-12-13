namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.Notifications;

    public class NotificationsService : INotificationsService
    {
        private readonly IDeletableEntityRepository<Notification> notificationsRepository;
        private readonly IDeletableEntityRepository<Vet> vetsRepository;

        public NotificationsService(IDeletableEntityRepository<Notification> notificationsRepository, IDeletableEntityRepository<Vet> vetsRepository)
        {
            this.notificationsRepository = notificationsRepository;
            this.vetsRepository = vetsRepository;
        }

        public async Task CreateNotificationForOwnerAsync(string ownerId, string content)
        {
            Notification notification = new Notification
            {
                OwnerId = ownerId,
                Content = content,
            };

            await this.notificationsRepository.AddAsync(notification);
            await this.notificationsRepository.SaveChangesAsync();
        }

        public async Task<SendNotificationViewModel> CreateNotificationForVetAsync(string vetId, string content)
        {
            Notification notification = new Notification
            {
                VetId = vetId,
                Content = content,
            };

            await this.notificationsRepository.AddAsync(notification);
            await this.notificationsRepository.SaveChangesAsync();

            SendNotificationViewModel model = new SendNotificationViewModel
            {
                VetId = vetId,
                Vet = this.vetsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == vetId),
                Content = content,
            };

            return model;
        }

        public List<T> GetOwnerNotifications<T>(string ownerId)
        {
            return this.notificationsRepository.AllAsNoTracking().Where(x => x.OwnerId == ownerId).To<T>().ToList();
        }

        public List<T> GetVetNotifications<T>(string vetId)
        {
            return this.notificationsRepository.AllAsNoTracking().Where(x => x.VetId == vetId).To<T>().ToList();
        }

        public async Task Delete(int id)
        {
            var notification = this.notificationsRepository.All().FirstOrDefault(x => x.Id == id);
            this.notificationsRepository.Delete(notification);
            await this.notificationsRepository.SaveChangesAsync();
        }
    }
}
