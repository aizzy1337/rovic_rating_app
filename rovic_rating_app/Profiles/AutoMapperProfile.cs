using AutoMapper;
using rovic_rating_app.Models;
using rovic_rating_app.Models.DTOs;

namespace rovic_rating_app.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Album, AlbumGetDTO>();
            CreateMap<AlbumPostDTO, Album>();
            CreateMap<AlbumUpdateDTO, Album>();

            CreateMap<Movie, MovieGetDTO>();
            CreateMap<MoviePostDTO, Movie>();
            CreateMap<MovieUpdateDTO, Movie>();

            CreateMap<Tag, TagGetDTO>();
            CreateMap<TagPostDTO, Tag>();

        }
    }
}
