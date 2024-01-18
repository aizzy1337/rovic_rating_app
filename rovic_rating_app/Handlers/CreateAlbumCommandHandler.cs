using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public class CreateAlbumCommandRequest : IRequest<AlbumGetDTO>
    {
        public AlbumPostDTO album { get; }

        public CreateAlbumCommandRequest(AlbumPostDTO album)
        {
            this.album = album;
        }
    }

    public class CreateAlbumCommandHandler
        : IRequestHandler<CreateAlbumCommandRequest, AlbumGetDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public CreateAlbumCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AlbumGetDTO> Handle(CreateAlbumCommandRequest request, CancellationToken ct)
        {
            var album = mapper.Map<Album>(request.album);
            _unitOfWork.Albums.Add(album);
            _unitOfWork.CompleteAsync();

            var albums = await _unitOfWork.Albums.GetAll();
            return mapper.Map<AlbumGetDTO>(albums.LastOrDefault());
        }
    }
}
