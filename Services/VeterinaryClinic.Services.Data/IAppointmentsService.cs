namespace VeterinaryClinic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VeterinaryClinic.Web.ViewModels.Appointments;

    public interface IAppointmentsService
    {
        Task CreateAppointmentAsync(RequestAppointmentViewModel model, string ownerId);

        IEnumerable<T> GetVetPendingAppointments<T>(string vetId);

        Task AcceptAsync(string appointmentId);

        IEnumerable<T> GetVetUpcomingAppointments<T>(string vetId);

        Task DeclineAsync(string appointmentId);

        Task CancelAsync(string appointmentId);

        Task StartAsync(string appointmentId);

        int GetAppointmentsInProgressCount(string vetId);

        T GetAppointmentInProgress<T>(string vetId);

        Task EndAsync(string appointmentId, DateTime endTime);

        T GetById<T>(string id);

        IEnumerable<T> GetVetPastAppointments<T>(string vetId);

        IEnumerable<T> GetOwnerUpcomingAppointments<T>(string ownerId);
    }
}
