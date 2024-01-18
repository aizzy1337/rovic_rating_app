using AutoMapper;
using MediatR;
using rovic_rating_app.Helpers;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;

namespace rovic_rating_app.Handlers
{
    public record SearchAlbumQueryRequest(string text) : IRequest<List<AlbumGetDTO>> { }

    public class SearchAlbumQueryHandler
        : IRequestHandler<SearchAlbumQueryRequest, List<AlbumGetDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public SearchAlbumQueryHandler(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<List<AlbumGetDTO>> Handle(SearchAlbumQueryRequest request, CancellationToken ct)
        {
            SearchAlbumHelper searchAlbumHelper = new SearchAlbumHelper(configuration);
            var result = await searchAlbumHelper.Search(request.text);

            return mapper.Map<List<AlbumGetDTO>>(result);
        }
    }
}
