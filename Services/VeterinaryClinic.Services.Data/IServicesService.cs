namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VeterinaryClinic.Web.ViewModels.Vets;

    public interface IServicesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        string GetNameById(int id);

        IEnumerable<T> GetAllServicesWhichAVetDoesNotHave<T>(string vetId);

        Task AddServiceToVet(AddServiceToVetInputModel input);
    }
}
