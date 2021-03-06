﻿namespace VeterinaryClinic.Services.Data
{
    using System.Collections.Generic;

    public interface IGalleryService
    {
        IEnumerable<T> GetAllForAPage<T>(int page);

        int GetCount();
    }
}
