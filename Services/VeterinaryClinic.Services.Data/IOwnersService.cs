using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;

namespace VeterinaryClinic.Services.Data
{
    public interface IOwnersService
    {
        Task CreateOwnerAsync(ApplicationUser user, string firstName, string lastName, string profilePictureUrl, string city);
    }
}
