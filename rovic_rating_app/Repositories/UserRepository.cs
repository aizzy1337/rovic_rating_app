using rovic_rating_app.Data;
using rovic_rating_app.Models;
using rovic_rating_app.Repositories.Interfaces;

namespace rovic_rating_app.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
