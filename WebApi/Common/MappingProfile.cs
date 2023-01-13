using AutoMapper;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.Application.AuthorOperations.Queries;
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
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => (src.Author.Name + " " + src.Author.Surname))
            );

        CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => (src.Author.Name + " " + src.Author.Surname))
            );
        CreateMap<UpdateBookModel, Book>();

        //Genre Mappers
        CreateMap<Genre, GenreViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();
        CreateMap<CreateGenreModel, Genre>();
        CreateMap<UpdateGenreModel, Genre>();

        //Author Mappers
        CreateMap<Author, AuthorViewModel>();
        CreateMap<Author, AuthorDetailViewModel>();
        CreateMap<CreateAuthorModel, Author>();
        CreateMap<UpdateAuthorModel, Author>();
    }
}
