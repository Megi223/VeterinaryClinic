namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Services.Messaging;
    using VeterinaryClinic.Web.ViewModels.Appointments;

    [Authorize(Roles = GlobalConstants.VetRoleName)]
    [Area("Vet")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IVetsService vetsService;
        private readonly IPetsService petsService;
        private readonly IPetsMedicationsService petsMedicationsService;
        private readonly IEmailSender emailSender;

        public AppointmentsController(IAppointmentsService appointmentsService, IVetsService vetsService, IPetsService petsService, IPetsMedicationsService petsMedicationsService, IEmailSender emailSender)
        {
            this.appointmentsService = appointmentsService;
            this.vetsService = vetsService;
            this.petsService = petsService;
            this.petsMedicationsService = petsMedicationsService;
            this.emailSender = emailSender;
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
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            await this.appointmentsService.AcceptAsync(id);
            var appointment = this.appointmentsService.GetById<SendEmailAppointmentViewModel>(id);
            var html = $"<h3>Your appointment with {appointment.VetFullName} at {TimeZoneInfo.ConvertTimeFromUtc(appointment.StartTime, zone)} has been accepted. We'll wait for you in MK clinic!</h3>";
            await this.emailSender.SendEmailAsync("mkvetclinic@gmail.com", "MK", appointment.OwnerUserEmail, "Accepted appointment", html);
            return this.RedirectToAction("Upcoming");
        }

        public async Task<IActionResult> Decline(string id)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            var appointment = this.appointmentsService.GetById<SendEmailAppointmentViewModel>(id);
            var html = $"<h3>Your appointment with {appointment.VetFullName} at {TimeZoneInfo.ConvertTimeFromUtc(appointment.StartTime, zone)} has been declined. Please request a new one!</h3>";
            await this.emailSender.SendEmailAsync("mkvetclinic@gmail.com", "MK", appointment.OwnerUserEmail, "Declined appointment", html);
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
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            var appointment = this.appointmentsService.GetById<SendEmailAppointmentViewModel>(id);
            var html = $"<h3>{appointment.VetFullName} has cancelled your appointment {TimeZoneInfo.ConvertTimeFromUtc(appointment.StartTime, zone)} due to personal reasons. Please request a new one!</h3>";
            await this.emailSender.SendEmailAsync("mkvetclinic@gmail.com", "MK", appointment.OwnerUserEmail, "Cancelled appointment", html); ;
            await this.appointmentsService.CancelAsync(id);
            return this.RedirectToAction("Upcoming");
        }

        public async Task<IActionResult> Start(string id)
        {
            var appointment = this.appointmentsService.GetById<PendingAppointmentViewModel>(id);
            if (DateTime.UtcNow < appointment.StartTime)
            {
                this.TempData["EarlyStart"] = "You cannot start the appointment before the arranged hour!";
                return this.RedirectToAction("Upcoming");
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vetId = this.vetsService.GetVetId(userId);
            var appointmentsInProgressCount = this.appointmentsService.GetAppointmentsInProgressCount(vetId);
            if (appointmentsInProgressCount == 1)
            {
                this.TempData["AppointmentProgress"] = "You are already in an appointment. You cannot start a new one!";
                var viewModel = this.appointmentsService.GetAppointmentInProgress<AppointmentInProgressViewModel>(vetId);
                return this.View(viewModel);
            }

            await this.appointmentsService.StartAsync(id);
            var model = this.appointmentsService.GetAppointmentInProgress<AppointmentInProgressViewModel>(vetId);
            return this.View(model);
        }

        public async Task<IActionResult> Diagnose(AppointmentInProgressViewModel model)
        {
            await this.petsService.SetDiagnoseAsync(model.PetDiagnoseDescription, model.PetDiagnoseName, model.PetId);
            var viewModel = this.appointmentsService.GetAppointmentInProgress<AppointmentInProgressViewModel>(model.VetId);
            this.TempData["SuccessfulDiagnose"] = "You successfully wrote the diagnosis.";
            return this.View("Start", viewModel);
        }

        public async Task<IActionResult> End(string id)
        {
            await this.appointmentsService.EndAsync(id, DateTime.UtcNow);
            return this.RedirectToAction("Upcoming");
        }

        public IActionResult Medication(string id)
        {
            var model = new PrescribeMedicationViewModel();
            model.PetId = id;
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Medication(PrescribeMedicationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["InvalidMedication"] = "Please provide valid madication";
                return this.View(model);
            }

            await this.petsMedicationsService.PrescribeMedicationAsync(model);
            return this.RedirectToAction("Current");
        }

        public IActionResult Current()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vetId = this.vetsService.GetVetId(userId);
            var viewModel = this.appointmentsService.GetAppointmentInProgress<AppointmentInProgressViewModel>(vetId);
            return this.View("Start", viewModel);
        }

        public async Task<IActionResult> Stop(int id)
        {
            await this.petsMedicationsService.EndMedicationAsync(id);
            return this.RedirectToAction("Current");
        }

        public IActionResult Past()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vetId = this.vetsService.GetVetId(userId);
            var viewModel = this.appointmentsService.GetVetPastAppointments<PastAppointmentViewModel>(vetId);
            return this.View(viewModel);
        }
    }
}
