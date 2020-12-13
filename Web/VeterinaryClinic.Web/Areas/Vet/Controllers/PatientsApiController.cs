namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Vets;

    [Route("api/[controller]")]
    [ApiController]
    public class PatientsApiController : ControllerBase
    {
        private readonly IVetsService vetsService;

        public PatientsApiController(IVetsService vetsService)
        {
            this.vetsService = vetsService;
        }

        [HttpPost]
        [Route("/api/patientsapi")]
        public IActionResult GetPatients()
        {
            try
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string vetId = this.vetsService.GetVetId(userId);
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var patientData = this.vetsService.GetVetsPatients<VetPatientViewModel>(vetId);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    patientData = patientData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    patientData = patientData.Where(m => m.Name.Contains(searchValue)
                                                || m.OwnerFirstName.Contains(searchValue)
                                                || m.OwnerLastName.Contains(searchValue));
                }

                recordsTotal = patientData.Count();
                var data = patientData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return this.Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
