namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class OwnerViewModelTest : IMapFrom<Owner>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string ProfilePicture { get; set; }
    }
}
