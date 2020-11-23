using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data
{
    public class OwnersService : IOwnersService
    {
        private readonly IDeletableEntityRepository<Owner> ownersRepository;

        public OwnersService(IDeletableEntityRepository<Owner> ownersRepository)
        {
            this.ownersRepository = ownersRepository;
        }

        
    }
}
