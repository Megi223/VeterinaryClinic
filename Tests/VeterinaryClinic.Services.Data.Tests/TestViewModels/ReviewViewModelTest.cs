using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    public class ReviewViewModelTest : IMapFrom<Review>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string OwnerId { get; set; }
    }
}
