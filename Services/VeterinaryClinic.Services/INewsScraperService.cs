namespace VeterinaryClinic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INewsScraperService
    {
        Task PopulateDbWithNews();
    }
}
