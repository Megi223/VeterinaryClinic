using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VeterinaryClinic.Web.ViewModels.ChatMessages
{
    public class SendChatMessageInputModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string UserId { get; set; }

        public string CallerId { get; set; }
    }
}
