using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record GetTagsByAlbumRequest(int albumId) : IRequest<List<TagGetDTO>> { }

    public class GetTagsByAlbumHandler
        : IRequestHandler<GetTagsByAlbumRequest, List<TagGetDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetTagsByAlbumHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<TagGetDTO>> Handle(GetTagsByAlbumRequest request, CancellationToken ct)
        {
            var allAlbumTags = await _unitOfWork.AlbumTags.GetAll();
            var albumTags = allAlbumTags.Where(at => at.AlbumId == request.albumId).ToList();
            var allTags = await _unitOfWork.Tags.GetAll();

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

            return mapper.Map<List<TagGetDTO>>(result);
        }
    }
}
