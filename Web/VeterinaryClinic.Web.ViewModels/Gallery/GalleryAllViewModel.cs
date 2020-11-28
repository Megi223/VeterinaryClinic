namespace VeterinaryClinic.Web.ViewModels.Gallery
{
    using VeterinaryClinic.Services.Mapping;

    public class GalleryAllViewModel : IMapFrom<VeterinaryClinic.Data.Models.Gallery>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
