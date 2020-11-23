using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploudAsync(IFormFile file);
    }
}
