namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.Hubs;
    using VeterinaryClinic.Web.ViewModels.Appointments;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly INotificationsService notificationsService;
        private readonly IOwnersService ownersService;
        private readonly IUsersService usersService;

        public AppointmentsController(IAppointmentsService appointmentsService, IOwnersService ownersService, INotificationsService notificationsService, IUsersService usersService)
        {
            this.appointmentsService = appointmentsService;
            this.ownersService = ownersService;
            this.notificationsService = notificationsService;
            this.usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> RequestAppointment(RequestAppointmentViewModel input)
        {
            var startTime = new DateTime(input.Date.Year, input.Date.Month, input.Date.Day, input.Time.Hour, input.Time.Minute, input.Time.Second);
            if (startTime.ToUniversalTime() <= DateTime.UtcNow)
            {
                this.TempData["InvalidDate"] = "Please select a valid date";
                return this.RedirectToAction("Contact", "Home", new { area = string.Empty });
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(userId);

            await this.appointmentsService.CreateAppointmentAsync(input, ownerId);
            this.TempData["SuucessfulRequest"] = "You have successfully requested an appointment";
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        public IActionResult MyAppointments()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(userId);
            var viewModel = this.appointmentsService.GetOwnerUpcomingAppointments<OwnerAppointmentViewModel>(ownerId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Cancel(string id)
        {
            var appointment = this.appointmentsService.GetById<CancelAppointmentViewModel>(id);
            await this.appointmentsService.CancelAsync(id);
            string content = $"Your appointment with {appointment.OwnerFullName} and pet {appointment.PetName} has been cancelled by the owner.";
            var notification = await this.notificationsService.CreateNotificationForVetAsync(appointment.VetId, content);

            return this.RedirectToAction("MyAppointments");
        }
    }
}
