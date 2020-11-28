namespace VeterinaryClinic.Web.ViewModels.Vets
{
    using System;

    using Ganss.XSS;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class VetCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string OwnerFirstName { get; set; }

        public string OwnerProfilePicture { get; set; }
    }
}
