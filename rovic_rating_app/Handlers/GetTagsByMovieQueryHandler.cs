using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;
using System.Runtime.ConstrainedExecution;

namespace rovic_rating_app.Handlers
{
    public record GetTagsByMovieRequest(int movieId) : IRequest<List<TagGetDTO>> { }
    
    public class GetTagsByMovieHandler
        : IRequestHandler<GetTagsByMovieRequest, List<TagGetDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetTagsByMovieHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<TagGetDTO>> Handle(GetTagsByMovieRequest request, CancellationToken ct)
        {
            var allMovieTags = await _unitOfWork.MovieTags.GetAll();
            var movieTags = allMovieTags.Where(mt => mt.MovieId == request.movieId).ToList(); 
            var allTags = await _unitOfWork.Tags.GetAll();

            List<Tag> result = new List<Tag>();
            foreach ( var tag in allTags)
            {
                foreach (var movieTag in movieTags)
                {
                    if (tag.Id == movieTag.TagId)
                    {
                        result.Add(tag);
                    }
                }
            }

            return mapper.Map<List<TagGetDTO>>(result);
        }
    }
}
