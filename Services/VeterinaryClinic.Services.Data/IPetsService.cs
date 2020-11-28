using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Web.ViewModels.Pets;

namespace VeterinaryClinic.Services.Data
{
    public interface IPetsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page, string ownerId);

        Task AddPetAsync(string userId, AddPetInputModel model, string photoUrl);

        int GetCountForOwner(string ownerId);

        T GetById<T>(string id);

        Task<string> DeterminePhotoUrl(IFormFile inputImage, string typeOfAnimal);
    }
}
