namespace VeterinaryClinic.Services.Data
{
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Models;

    public interface IOwnersService
    {
        Task CreateOwnerAsync(ApplicationUser user, string firstName, string lastName, string profilePictureUrl, string city);

        string GetOwnerId(string userId);
    }
}
