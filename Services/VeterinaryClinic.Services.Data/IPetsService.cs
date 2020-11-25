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
        IEnumerable<T> GetPets<T>(string userId);

        Task AddPetAsync(string userId, AddPetInputModel model, string photoUrl);
    }
}
