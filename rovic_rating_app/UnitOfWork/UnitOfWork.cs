using rovic_rating_app.Data;
using rovic_rating_app.Repositories;
using rovic_rating_app.Repositories.Interfaces;

namespace rovic_rating_app.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;

        public IUserRepository Users { get; private set; }
        public IAlbumRepository Albums { get; private set; }
        public IMovieRepository Movies { get; private set; }
        public ITagRepository Tags { get; private set; }
        public IAlbumTagRepository AlbumTags { get; private set; }
        public IMovieTagRepository MovieTags { get; private set; }

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Albums = new AlbumRepository(context);
            Movies = new MovieRepository(context);
            Tags = new TagRepository(context);
            AlbumTags = new AlbumTagRepository(context);
            MovieTags = new MovieTagRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
