using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Services.Data
{
    public interface IVetsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        T GetById<T>(string id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        string GetServices(string vetId);
    }
}
