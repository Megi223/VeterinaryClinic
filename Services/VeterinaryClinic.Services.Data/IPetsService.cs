using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;

namespace VeterinaryClinic.Services.Data
{
    public interface IPetsService
    {
        IEnumerable<T> GetPets<T>(string userId);
    }
}
