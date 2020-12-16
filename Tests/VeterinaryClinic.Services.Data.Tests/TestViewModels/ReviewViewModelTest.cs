namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class ReviewViewModelTest : IMapFrom<Review>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string OwnerId { get; set; }
    }
}
