namespace VeterinaryClinic.Data.Models
{
    using VeterinaryClinic.Data.Common.Models;

    public class Gallery : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
