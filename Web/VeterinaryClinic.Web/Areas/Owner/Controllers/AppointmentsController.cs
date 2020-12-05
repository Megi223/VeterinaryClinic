namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Appointments;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IOwnersService ownersService;

        public AppointmentsController(IAppointmentsService appointmentsService, IOwnersService ownersService)
        {
            this.appointmentsService = appointmentsService;
            this.ownersService = ownersService;
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
            return this.RedirectToAction("Index", "Home", new { area = "" });
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
            await this.appointmentsService.CancelAsync(id);

            // TODO: Send notification to vet
            return this.RedirectToAction("MyAppointments");
        }
    }
}
