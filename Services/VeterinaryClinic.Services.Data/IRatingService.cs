namespace VeterinaryClinic.Services.Data
{
    using System.Threading.Tasks;

    public interface IRatingService
    {
        Task SetRatingAsync(string vetId, string ownerId, byte score);

        float GetAverageRatings(string vetId);
    }
}
