namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using VeterinaryClinic.Web.ViewModels.Pets;

    public interface IPetsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page, string ownerId);

        Task AddPetAsync(string userId, AddPetInputModel model, string photoUrl);

        int GetCountForOwner(string ownerId);

        T GetById<T>(string id);

        Task<string> DeterminePhotoUrl(IFormFile inputImage, string typeOfAnimal);

        IEnumerable<T> GetPets<T>(string ownerId);

        Task SetDiagnoseAsync(string diagnoseDescription, string diagnoseName, string petId);

        Task EditAsync(EditPetViewModel edit);

        Task DeleteAsync(string id);
    }
}
