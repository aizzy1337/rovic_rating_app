using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public class UpdateAlbumCommandRequest : IRequest<AlbumUpdateDTO>
    {
        public AlbumUpdateDTO album { get; }

        public UpdateAlbumCommandRequest(AlbumUpdateDTO album)
        {
            this.album = album;
        }
    }

    public class UpdateAlbumCommandHandler
        : IRequestHandler<UpdateAlbumCommandRequest, AlbumUpdateDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public UpdateAlbumCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AlbumUpdateDTO> Handle(UpdateAlbumCommandRequest request, CancellationToken ct)
        {
            var album = mapper.Map<Album>(request.album);
            _unitOfWork.Albums.Update(mapper.Map<Album>(album));
            _unitOfWork.CompleteAsync();

            return request.album;
        }
    }
}
