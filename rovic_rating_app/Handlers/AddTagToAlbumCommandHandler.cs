using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record AddTagToAlbumRequest(int albumId, int tagId) : IRequest<bool> { }

    public class AddTagToAlbumCommandHandler
        : IRequestHandler<AddTagToAlbumRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTagToAlbumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddTagToAlbumRequest request, CancellationToken ct)
        {
            await _unitOfWork.AlbumTags.Add(new AlbumTag()
            {
                AlbumId = request.albumId,
                TagId = request.tagId
            });

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
