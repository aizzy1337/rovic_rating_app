using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record GetMovieByUserIdRequest(int userId) : IRequest<List<MovieGetDTO>> { }

    public class GetMovieByUserIdHandler
        : IRequestHandler<GetMovieByUserIdRequest, List<MovieGetDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetMovieByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<MovieGetDTO>> Handle(GetMovieByUserIdRequest request, CancellationToken ct)
        {
            var movies = await _unitOfWork.Movies.GetAll();
            var userMovies = movies.Where(movie => movie.UserId == request.userId).ToList();

            var allMovieTags = await _unitOfWork.MovieTags.GetAll();
            var allTags = await _unitOfWork.Tags.GetAll();

            var mappedMovies = mapper.Map<List<MovieGetDTO>>(userMovies);

            foreach (var movie in mappedMovies)
            {
                var movieTags = allMovieTags.Where(mt => mt.MovieId == movie.Id).ToList();
                List<Tag> result = new List<Tag>();
                foreach (var tag in allTags)
                {
                    foreach (var movieTag in movieTags)
                    {
                        if (tag.Id == movieTag.TagId)
                        {
                            result.Add(tag);
                        }
                    }
                }
                movie.Tags = result;
            }

            return mappedMovies;
        }
    }
}
