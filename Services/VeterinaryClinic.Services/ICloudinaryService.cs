namespace VeterinaryClinic.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploudAsync(IFormFile file);
    }
}
