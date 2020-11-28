namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;

    public interface IServicesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        string GetNameById(int id);
    }
}
