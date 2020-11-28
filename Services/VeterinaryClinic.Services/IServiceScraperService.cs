namespace VeterinaryClinic.Services
{
    using System.Threading.Tasks;

    public interface IServiceScraperService
    {
        Task PopulateDbWithServices();
    }
}
