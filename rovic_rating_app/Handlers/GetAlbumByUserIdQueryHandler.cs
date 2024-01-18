using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record GetAlbumByUserIdRequest(int userId) : IRequest<List<AlbumGetDTO>> { }

    public class GetAlbumByUserIdHandler
        : IRequestHandler<GetAlbumByUserIdRequest, List<AlbumGetDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAlbumByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AlbumGetDTO>> Handle(GetAlbumByUserIdRequest request, CancellationToken ct)
        {
            var albums = await _unitOfWork.Albums.GetAll();
            var userAlbums = albums.Where(album => album.UserId == request.userId).ToList();

            var allAlbumTags = await _unitOfWork.AlbumTags.GetAll();
            var allTags = await _unitOfWork.Tags.GetAll();

            var mappedAlbums = _mapper.Map<List<AlbumGetDTO>>(userAlbums);

            foreach (var album in mappedAlbums)
            {
                var albumTags = allAlbumTags.Where(at => at.AlbumId == album.Id).ToList();
                List<Tag> result = new List<Tag>();
                foreach (var tag in allTags)
                {
                    foreach (var albumTag in albumTags)
                    {
                        if (tag.Id == albumTag.TagId)
                        {
                            result.Add(tag);
                        }
                    }
                }
                album.Tags = result;
            }

            return mappedAlbums;
        }
    }
}
