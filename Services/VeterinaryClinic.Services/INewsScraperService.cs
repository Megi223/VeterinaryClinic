namespace VeterinaryClinic.Services
{
    using System.Threading.Tasks;

    public interface INewsScraperService
    {
        Task PopulateDbWithNews();
    }
}
