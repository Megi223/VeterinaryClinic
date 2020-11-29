using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Reviews
{
    public class ReviewViewModel
    {
        public IEnumerable<SingleReviewViewModel> Reviews { get; set; }
    }
}
