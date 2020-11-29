using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Appointments;

namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(userId);

            await this.appointmentsService.CreateAppointmentAsync(input, ownerId);
            return this.RedirectToAction("Index","Home",new { area = "" });
        }
    }
}
