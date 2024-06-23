using AdaSoftLibrary.Application.Books.Commands;
using AdaSoftLibrary.Application.Books.Queries;
using AdaSoftLibrary.Domain.Entities;
using AutoMapper;

namespace AdaSoftLibrary.Application.Books.Mapping;

internal class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, GetBookResponse>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => (src.Borrowed != null ? src.Borrowed.FirstName : null)))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => (src.Borrowed != null ? src.Borrowed.LastName : null)))
            .ForMember(dest => dest.BorrowedFrom, opt => opt.MapFrom(src => (src.Borrowed != null ? src.Borrowed.FromDate : null)))
            .ForMember(dest => dest.IsBorrowed, opt => opt.MapFrom(src => src.IsBorrowed))
            ;

        CreateMap<CreateBook.Command, Book>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            ;

        CreateMap<UpdateBook.Command, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            ;
    }
}
