namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;

    public interface INewsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        T GetById<T>(int id);

        int GetCount();
    }
}
