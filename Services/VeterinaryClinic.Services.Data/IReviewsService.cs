using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Services.Data
{
    public interface IReviewsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        int GetCount();
    }
}
