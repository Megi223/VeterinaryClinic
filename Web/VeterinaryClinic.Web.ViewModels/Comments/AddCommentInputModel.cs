﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Web.ViewModels.Comments
{
    public class AddCommentInputModel
    {
        public string VetId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
