using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using WebApi.Application.UserOperations.Queries.GetUsers;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name)).ForMember(dest => dest.Author, opt=>opt.MapFrom(src=>src.Author.AuthorName+ " "+ src.Author.AuthorSurname));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name)).ForMember(dest => dest.Author, opt=>opt.MapFrom(src=>src.Author.AuthorName+ " "+ src.Author.AuthorSurname));
            CreateMap<UpdateBookModel, Book>();

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<UpdateGenreModel, Genre>();

            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();

            CreateMap<CreateUserModel , User>();
             CreateMap<User, UsersViewModel>();
            CreateMap<User, UserDetailViewModel>();
            CreateMap<UpdateUserModel, User>();

        }
    }
}