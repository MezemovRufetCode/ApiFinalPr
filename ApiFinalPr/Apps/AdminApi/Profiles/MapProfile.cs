using ApiFinalPr.Apps.AdminApi.DTOs.AuthorDtos;
using ApiFinalPr.Apps.AdminApi.DTOs.BookDtos;
using ApiFinalPr.Apps.AdminApi.DTOs.GenreDtos;
using ApiFinalPr.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.AdminApi.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Author, AuthorGetDto>();
            CreateMap<Author, AuthorInProductGetDto>();
            CreateMap<Genre, GenreGetDto>();
            CreateMap<Genre, GenreInProductGetDto>();
            CreateMap<Book, BookGetDto>();
        }
    }
}
