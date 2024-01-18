using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public class CreateMovieCommandRequest : IRequest<MovieGetDTO>
    {
        public MoviePostDTO movie { get; }

        public CreateMovieCommandRequest(MoviePostDTO movie)
        {
            this.movie = movie;
        }
    }

    public class CreateMovieCommandHandler
        : IRequestHandler<CreateMovieCommandRequest, MovieGetDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public CreateMovieCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<MovieGetDTO> Handle(CreateMovieCommandRequest request, CancellationToken ct)
        {
            _unitOfWork.Movies.Add(mapper.Map<Movie>(request.movie));
            _unitOfWork.CompleteAsync();

            var movies = await _unitOfWork.Movies.GetAll();
            return mapper.Map<MovieGetDTO>(movies.LastOrDefault());
        }
    }
}
