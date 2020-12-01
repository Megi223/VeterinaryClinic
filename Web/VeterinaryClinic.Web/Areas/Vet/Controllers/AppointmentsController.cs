namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Appointments;

    [Authorize(Roles = GlobalConstants.VetRoleName)]
    [Area("Vet")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IVetsService vetsService;
        private readonly IPetsService petsService;


        public AppointmentsController(IAppointmentsService appointmentsService, IVetsService vetsService, IPetsService petsService)
        {
            this.appointmentsService = appointmentsService;
            this.vetsService = vetsService;
            this.petsService = petsService;
        }

        public IActionResult Pending()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vetId = this.vetsService.GetVetId(userId);
            var viewModel = this.appointmentsService.GetVetPendingAppointments<PendingAppointmentViewModel>(vetId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Accept(string id)
        {
            await this.appointmentsService.AcceptAsync(id);
            return this.RedirectToAction("Upcoming");
        }

        public async Task<IActionResult> Decline(string id)
        {
            await this.appointmentsService.DeclineAsync(id);
            return this.RedirectToAction("Pending");
        }

        public IActionResult Upcoming()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vetId = this.vetsService.GetVetId(userId);
            var viewModel = this.appointmentsService.GetVetUpcomingAppointments<UpcomingAppointmentViewModel>(vetId);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Cancel(string id)
        {
            await this.appointmentsService.CancelAsync(id);
            return this.RedirectToAction("Upcoming");
        }

        public async Task<IActionResult> Start(string id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vetId = this.vetsService.GetVetId(userId);
            var appointmentsInProgressCount = this.appointmentsService.GetAppointmentsInProgressCount(vetId);
            if (appointmentsInProgressCount == 1)
            {
                this.TempData["AppointmentProgress"] = "You are already in an appointment. You cannot start new one!";
                var viewModel = this.appointmentsService.GetAppointmentInProgress<AppointmentInProgressViewModel>(vetId);
                return this.View(viewModel);
            }
            await this.appointmentsService.StartAsync(id);
            var model = this.appointmentsService.GetAppointmentInProgress<AppointmentInProgressViewModel>(vetId);
            return this.View(model);

        }

        public async Task<IActionResult> Diagnose(AppointmentInProgressViewModel model)
        {
            await this.petsService.SetDiagnoseAsync(model.PetDiagnoseDescription,model.PetDiagnoseName,model.PetId);
            var viewModel = this.appointmentsService.GetAppointmentInProgress<AppointmentInProgressViewModel>(model.VetId);
            return this.View("Start",viewModel);
        }
    }
}
