using MediatR;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record DeleteMovieRequest(int id) : IRequest<bool> { }

    public class DeleteMovieHandler
        : IRequestHandler<DeleteMovieRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMovieHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteMovieRequest request, CancellationToken ct)
        {
            var movie = await _unitOfWork.Movies.GetById(request.id);
            var allTags = await _unitOfWork.MovieTags.GetAll();
            var relatedTags = allTags.Where(t => t.MovieId == request.id);

            if (movie == null)
            {
                return false;
            }

            foreach (var tag in relatedTags)
            {
                await _unitOfWork.MovieTags.Remove(tag);
            }

            await _unitOfWork.Movies.Remove(movie);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
