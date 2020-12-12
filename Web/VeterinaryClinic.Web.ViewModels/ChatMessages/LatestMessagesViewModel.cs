using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.ChatMessages
{
    public class LatestMessagesViewModel : IMapFrom<ChatMessage>
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string OwnerProfilePicture { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerFullName => this.OwnerFirstName + " " + this.OwnerLastName;

        public string VetId { get; set; }

        public string VetProfilePicture { get; set; }

        public string VetFirstName { get; set; }

        public string VetLastName { get; set; }

        public string VetFullName => this.VetFirstName + " " + this.VetLastName;

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
