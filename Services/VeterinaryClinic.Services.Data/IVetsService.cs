namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using VeterinaryClinic.Web.ViewModels.Vets;

    public interface IVetsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        T GetById<T>(string id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        string GetServices(string vetId);

        string GetVetId(string userId);

        Task<string> DeterminePhotoUrl(IFormFile input);

        Task AddVetAsync(string userId, AddVetInputModel model, string photoUrl);

        IEnumerable<T> GetVetsPatientsForAPage<T>(string vetId, int page);

        int GetPatientsCount(string vetId);

        string GetNameById(string vetId);

        Task DeleteVet(string vetId);

        IEnumerable<T> GetServices<T>(string vetId);

        Task EditVet(EditVetInputModel input);

        IQueryable<T> GetVetsPatients<T>(string vetId);
    }
}
