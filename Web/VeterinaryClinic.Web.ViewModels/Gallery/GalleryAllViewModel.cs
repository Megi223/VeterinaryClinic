using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Gallery
{
    public class GalleryAllViewModel : IMapFrom<VeterinaryClinic.Data.Models.Gallery>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
