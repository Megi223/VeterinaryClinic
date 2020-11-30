using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Appointments;

namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    [Authorize(Roles = GlobalConstants.VetRoleName)]
    [Area("Vet")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IVetsService vetsService;

        public AppointmentsController(IAppointmentsService appointmentsService, IVetsService vetsService)
        {
            this.appointmentsService = appointmentsService;
            this.vetsService = vetsService;
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
    }
}
