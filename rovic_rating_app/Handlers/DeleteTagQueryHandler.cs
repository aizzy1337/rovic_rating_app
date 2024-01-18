using MediatR;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record DeleteTagRequest(int id) : IRequest<bool> { }

    public class DeleteTagHandler
        : IRequestHandler<DeleteTagRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTagHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTagRequest request, CancellationToken ct)
        {
            var tag = await _unitOfWork.Tags.GetById(request.id);

            if (tag == null)
            {
                return false;
            }

            if (tag.IsMovieTag)
            {
                var allTags = await _unitOfWork.MovieTags.GetAll();
                var relatedTags = allTags.Where(t => t.TagId == request.id);

                foreach (var t in relatedTags)
                {
                    await _unitOfWork.MovieTags.Remove(t);
                }
            }
            else
            {
                var allTags = await _unitOfWork.AlbumTags.GetAll();
                var relatedTags = allTags.Where(t => t.TagId == request.id);

                foreach (var t in relatedTags)
                {
                    await _unitOfWork.AlbumTags.Remove(t);
                }
            }

            await _unitOfWork.Tags.Remove(tag);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
