using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    public class GalleryViewModelTest : IMapFrom<Gallery>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
