using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Services.Data
{
    public interface IRatingService
    {
        Task SetRatingAsync(string vetId, string ownerId, byte score);

        float GetAverageRatings(string vetId);
    }
}
