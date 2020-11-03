namespace VeterinaryClinic.Data.Models
{
    using System;

    using VeterinaryClinic.Data.Common.Models;

    public class DosingTime : BaseDeletableModel<int>
    {
        public DateTime Time { get; set; }
    }
}
