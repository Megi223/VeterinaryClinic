namespace VeterinaryClinic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IServicesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        string GetNameById(int id);
    }
}
