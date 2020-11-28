using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(VeterinaryClinic.Web.Areas.Identity.IdentityHostingStartup))]

namespace VeterinaryClinic.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
