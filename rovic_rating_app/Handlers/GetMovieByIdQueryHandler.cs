using AutoMapper;
using MediatR;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record GetMovieByIdRequest(int id) : IRequest<MovieGetDTO> { }

    public class GetMovieByIdHandler
        : IRequestHandler<GetMovieByIdRequest, MovieGetDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetMovieByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<MovieGetDTO> Handle(GetMovieByIdRequest request, CancellationToken ct)
        {
            var movie = await _unitOfWork.Movies.GetById(request.id);

            return mapper.Map<MovieGetDTO>(movie);
        }
    }
}
