using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public class UpdateMovieCommandRequest : IRequest<MovieUpdateDTO>
    {
        public MovieUpdateDTO movie { get; }

        public UpdateMovieCommandRequest(MovieUpdateDTO movie)
        {
            this.movie = movie;
        }
    }

    public class UpdateMovieCommandHandler
        : IRequestHandler<UpdateMovieCommandRequest, MovieUpdateDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public UpdateMovieCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<MovieUpdateDTO> Handle(UpdateMovieCommandRequest request, CancellationToken ct)
        {
            var movie = mapper.Map<Movie>(request.movie);
            await _unitOfWork.Movies.Update(movie);
            await _unitOfWork.CompleteAsync();

            return request.movie;
        }
    }
}
