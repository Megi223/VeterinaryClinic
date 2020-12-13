namespace VeterinaryClinic.Services.Data
{
    using System.Threading.Tasks;

    using VeterinaryClinic.Web.ViewModels.Appointments;

    public interface IPetsMedicationsService
    {
        Task PrescribeMedicationAsync(PrescribeMedicationViewModel model);

        Task EndMedicationAsync(int id);
    }
}
