namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;

    public interface IReviewsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        int GetCount();
    }
}
