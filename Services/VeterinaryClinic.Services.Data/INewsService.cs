namespace VeterinaryClinic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface INewsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        T GetById<T>(int id);

        int GetCount();
    }
}
