using AutoMapper;
using MediatR;
using RestSharp;
using rovic_rating_app.Helpers;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;
using System.Configuration;
using System.Web;

namespace rovic_rating_app.Handlers
{
    public record SearchMovieQueryRequest(string text) : IRequest<List<MovieGetDTO>> { }

    public class SearchMovieQueryHandler
        : IRequestHandler<SearchMovieQueryRequest, List<MovieGetDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public SearchMovieQueryHandler(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<List<MovieGetDTO>> Handle(SearchMovieQueryRequest request, CancellationToken ct)
        {
            SearchMovieHelper searchMovieHelper = new SearchMovieHelper(configuration);
            var result = await searchMovieHelper.Search(request.text);

            return mapper.Map<List<MovieGetDTO>>(result);   
        }
    }
}
