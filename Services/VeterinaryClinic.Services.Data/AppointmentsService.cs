using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.ViewModels.Appointments;
using VeterinaryClinic.Data.Models.Enumerations;

namespace VeterinaryClinic.Services.Data
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;

        public AppointmentsService(IDeletableEntityRepository<Appointment> appointmentsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
        }

        public async Task CreateAppointmentAsync(RequestAppointmentViewModel model, string ownerId)
        {
            var startTime = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, model.Time.Hour, model.Time.Minute, model.Time.Second);
            Appointment appointment = new Appointment
            {
                Date = model.Date,
                StartTime = startTime.ToUniversalTime(),
                OwnerId = ownerId,
                PetId = model.PetId,
                Subject = model.Subject,
                VetId = model.VetId,
            };

            await this.appointmentsRepository.AddAsync(appointment);
            await this.appointmentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetVetPendingAppointments<T>(string vetId)
        {
            IQueryable<Appointment> pendingAppointments= this.appointmentsRepository.AllAsNoTracking().Where(x => x.VetId == vetId && x.IsAcceptedByVet == false);

            return pendingAppointments.To<T>().ToList();
        }

        public async Task AcceptAsync(string appointmentId)
        {
            this.appointmentsRepository.All().Where(x => x.Id == appointmentId).FirstOrDefault().IsAcceptedByVet = true;
            await this.appointmentsRepository.SaveChangesAsync();
        }

        public async Task DeclineAsync(string appointmentId)
        {
            this.appointmentsRepository.All().Where(x => x.Id == appointmentId).FirstOrDefault().IsDeleted = true;
            await this.appointmentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetVetUpcomingAppointments<T>(string vetId)
        {
            IQueryable<Appointment> query =
                this.appointmentsRepository.All().Where(x => x.VetId == vetId && x.IsAcceptedByVet == true && x.IsCancelledByOwner == false && x.Status == Status.Upcoming);

            return query.To<T>().ToList();
        }
    }
}
