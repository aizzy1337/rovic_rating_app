using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record GetAlbumTagsRequest(int userId) : IRequest<List<TagGetDTO>> { }

    public class GetAlbumTagsHandler
        : IRequestHandler<GetAlbumTagsRequest, List<TagGetDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetAlbumTagsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<TagGetDTO>> Handle(GetAlbumTagsRequest request, CancellationToken ct)
        {
            var tags = await _unitOfWork.Tags.GetAll();

            var albumTags = tags
                .Where(t => t.UserId == request.userId)
                .Where(t => t.IsMovieTag == false)
                .ToList();

            return mapper.Map<List<TagGetDTO>>(albumTags);
        }
    }
}
