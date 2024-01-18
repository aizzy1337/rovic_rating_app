using rovic_rating_app.Data;
using rovic_rating_app.Models;
using rovic_rating_app.Repositories.Interfaces;

namespace rovic_rating_app.Repositories
{
    public class MovieTagRepository : Repository<MovieTag>, IMovieTagRepository
    {
        private readonly DataContext _context;

        public MovieTagRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
