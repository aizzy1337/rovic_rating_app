using rovic_rating_app.Data;
using rovic_rating_app.Models;
using rovic_rating_app.Repositories.Interfaces;

namespace rovic_rating_app.Repositories
{
    public class AlbumTagRepository : Repository<AlbumTag>, IAlbumTagRepository
    {
        private readonly DataContext _context;

        public AlbumTagRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
