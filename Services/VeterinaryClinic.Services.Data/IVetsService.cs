using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Services.Data
{
    public interface IVetsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
