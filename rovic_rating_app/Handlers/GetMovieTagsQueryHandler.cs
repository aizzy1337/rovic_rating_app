using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
        public record GetMovieTagsRequest(int userId) : IRequest<List<TagGetDTO>> { }

        public class GetMovieTagsHandler
            : IRequestHandler<GetMovieTagsRequest, List<TagGetDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetMovieTagsHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

            public async Task<List<TagGetDTO>> Handle(GetMovieTagsRequest request, CancellationToken ct)
            {
                var tags = await _unitOfWork.Tags.GetAll();

                var movieTags = tags
                    .Where(t => t.UserId == request.userId)
                    .Where(t => t.IsMovieTag == true)
                    .ToList();

                return mapper.Map<List<TagGetDTO>>(movieTags);
            }
        }
}
