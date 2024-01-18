using MediatR;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record DeleteAlbumRequest(int id) : IRequest<bool> { }

    public class DeleteAlbumHandler
        : IRequestHandler<DeleteAlbumRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAlbumHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAlbumRequest request, CancellationToken ct)
        {
            var album = await _unitOfWork.Albums.GetById(request.id);
            var allTags = await _unitOfWork.AlbumTags.GetAll();
            var relatedTags = allTags.Where(t => t.AlbumId == request.id);

            if (album == null)
            {
                return false;
            }

            foreach (var tag in relatedTags)
            {
                await _unitOfWork.AlbumTags.Remove(tag);
            }

            await _unitOfWork.Albums.Remove(album);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
