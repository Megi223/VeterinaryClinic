namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;

    public interface IVetsService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        T GetById<T>(string id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        string GetServices(string vetId);

        string GetVetId(string userId);
    }
}
