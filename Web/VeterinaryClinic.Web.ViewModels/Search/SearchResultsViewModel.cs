using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Web.ViewModels.Search
{
    public class SearchResultsViewModel
    {
        public IEnumerable<VetsFoundViewModel> Vets { get; set; }

        public IEnumerable<ServicesFoundViewModel> Services { get; set; }

        public IEnumerable<NewsFoundViewModel> News { get; set; }

    }
}
