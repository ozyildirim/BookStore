using AutoMapper;
using WebApi.BookOperations.GetBookDetailQuery;
using WebApi.Models;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;

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
    }
}
