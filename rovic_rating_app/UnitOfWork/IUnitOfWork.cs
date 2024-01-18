using rovic_rating_app.Repositories.Interfaces;

namespace rovic_rating_app.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IAlbumRepository Albums { get; }
        IMovieRepository Movies { get; }
        ITagRepository Tags { get; }
        IAlbumTagRepository AlbumTags { get; }
        IMovieTagRepository MovieTags { get; }
        Task CompleteAsync();
    }
}
