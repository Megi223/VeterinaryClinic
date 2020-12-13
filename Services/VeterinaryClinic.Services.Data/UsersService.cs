namespace VeterinaryClinic.Services.Data
{
    using System.Linq;

    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;

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
