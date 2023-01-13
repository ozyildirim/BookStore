using AutoMapper;
using WebApi.Application.BookOperations.Commands;
using WebApi.Application.BookOperations.Queries;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Means CreateBookModel object could be mapped to Book entity object
        CreateMap<CreateBookModel, Book>();

        CreateMap<Book, BookDetailViewModel>()
            .ForMember(
                dest => dest.Genre,
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())
            );

        CreateMap<Book, BookViewModel>()
            .ForMember(
                dest => dest.Genre,
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())
            );
        CreateMap<UpdateBookModel, Book>();

        CreateMap<Genre, GenreViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();
        CreateMap<CreateGenreModel, Genre>();
        CreateMap<UpdateGenreModel, Genre>();
    }
}
