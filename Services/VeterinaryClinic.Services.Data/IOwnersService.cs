namespace VeterinaryClinic.Services.Data
{
    using System.Threading.Tasks;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Web.ViewModels.Reviews;

    public interface IOwnersService
    {
        Task CreateOwnerAsync(ApplicationUser user, string firstName, string lastName, string profilePictureUrl, string city);

        string GetOwnerId(string userId);

        Task WriteReviewAsync(string ownerId, ReviewInputModel input);
    }
}
