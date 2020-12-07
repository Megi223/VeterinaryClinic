using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;

namespace VeterinaryClinic.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> usersRepository;

        public UsersService(IRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public string GetUserUserName(string userId)
        {
            return this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == userId).UserName;
        }
    }
}
