using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Services.Data
{
    public interface ISearchService
    {
        List<string> SearchVet(string term);

        List<string> SearchServices(string term);

        List<string> SearchNews(string term);
    }
}
