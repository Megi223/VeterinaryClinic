namespace VeterinaryClinic.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchResultsViewModel
    {
        public IEnumerable<VetsFoundViewModel> Vets { get; set; }

        public IEnumerable<ServicesFoundViewModel> Services { get; set; }

        public IEnumerable<NewsFoundViewModel> News { get; set; }
    }
}
