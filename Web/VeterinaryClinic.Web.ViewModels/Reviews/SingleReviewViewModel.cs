namespace VeterinaryClinic.Web.ViewModels.Reviews
{
    using Ganss.XSS;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class SingleReviewViewModel : IMapFrom<Review>
    {
        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string OwnerProfilePicture { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerFullName => this.OwnerFirstName + " " + this.OwnerLastName;

        public string OwnerCity { get; set; }
    }
}
