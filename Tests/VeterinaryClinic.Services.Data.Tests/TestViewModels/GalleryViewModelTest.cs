namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class GalleryViewModelTest : IMapFrom<Gallery>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
