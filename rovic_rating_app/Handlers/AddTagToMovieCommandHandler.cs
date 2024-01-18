using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record AddTagToMovieRequest(int movieId, int tagId) : IRequest<bool> { }

    public class AddTagToMovieCommandHandler
        : IRequestHandler<AddTagToMovieRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTagToMovieCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddTagToMovieRequest request, CancellationToken ct)
        {
            await _unitOfWork.MovieTags.Add(new MovieTag()
            {
                MovieId = request.movieId,
                TagId = request.tagId
            });

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
