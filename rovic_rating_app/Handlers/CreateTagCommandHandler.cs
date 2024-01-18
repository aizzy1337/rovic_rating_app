using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public class CreateTagCommandRequest : IRequest<TagPostDTO>
    {
        public TagPostDTO tag { get; }

        public CreateTagCommandRequest(TagPostDTO tag)
        {
            this.tag = tag;
        }
    }

    public class CreateTagCommandHandler
        : IRequestHandler<CreateTagCommandRequest, TagPostDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public CreateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TagPostDTO> Handle(CreateTagCommandRequest request, CancellationToken ct)
        {
            var tag = mapper.Map<Tag>(request.tag);
            await _unitOfWork.Tags.Add(tag);
            await _unitOfWork.CompleteAsync();

            return request.tag;
        }
    }
}
