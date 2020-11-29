using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Reviews
{
    public class AllReviewsViewModel : IMapFrom<Review>
    {
        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string OwnerProfilePicture { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerFullName => this.OwnerFirstName + " " + this.OwnerLastName;

        public DateTime CreatedOn { get; set; }
    }
}
