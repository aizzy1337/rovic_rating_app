using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record GetAlbumByIdRequest(int id) : IRequest<AlbumGetDTO> { }

    public class GetAlbumByIdHandler
        : IRequestHandler<GetAlbumByIdRequest, AlbumGetDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAlbumByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AlbumGetDTO> Handle(GetAlbumByIdRequest request, CancellationToken ct)
        {
            var album = await _unitOfWork.Albums.GetById(request.id);

            return _mapper.Map<AlbumGetDTO>(album);
        }
    }
}
