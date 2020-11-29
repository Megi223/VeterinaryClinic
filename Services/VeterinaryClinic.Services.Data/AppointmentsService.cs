using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Web.ViewModels.Appointments;

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
            Appointment appointment = new Appointment
            {
                Date = model.Date,
                StartTime = model.Time,
                OwnerId = ownerId,
                PetId = model.PetId,
                Subject = model.Subject,
                VetId = model.VetId,
            };

            await this.appointmentsRepository.AddAsync(appointment);
            await this.appointmentsRepository.SaveChangesAsync();
        }
    }
}
