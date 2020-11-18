using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Services.Data
{
    public interface INewsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        T GetById<T>(int id);

        int GetCount();
    }
}
