﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Services
{
    public interface INewsScraperService
    {
        Task PopulateDbWithNews();
    }
}
