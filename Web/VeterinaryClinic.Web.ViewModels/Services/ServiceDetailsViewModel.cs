namespace VeterinaryClinic.Web.ViewModels.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class ServiceDetailsViewModel : IMapFrom<Service>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
