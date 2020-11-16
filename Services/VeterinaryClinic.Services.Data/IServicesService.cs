using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Services.Data
{
    public interface IServicesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);
    }
}
