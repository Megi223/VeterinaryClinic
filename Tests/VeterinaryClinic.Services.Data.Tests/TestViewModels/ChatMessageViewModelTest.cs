using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    public class ChatMessageViewModelTest : IMapFrom<ChatMessage>
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string VetId { get; set; }

        public bool IsRead { get; set; }

        public RoleName SenderRole { get; set; }

        public RoleName ReceiverRole { get; set; }

        public string Content { get; set; }
    }
}
