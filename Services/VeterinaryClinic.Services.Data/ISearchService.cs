namespace VeterinaryClinic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISearchService
    {
        List<string> SearchVet(string term);

        List<string> SearchServices(string term);

        List<string> SearchNews(string term);
    }
}
