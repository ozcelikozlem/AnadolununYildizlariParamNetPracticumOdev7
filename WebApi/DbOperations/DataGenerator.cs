using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context =new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre
                    {
                        Name="Personal Growth"
                    },
                    new Genre
                    {
                        Name="Science Fiction"
                    },
                    new Genre
                    {
                        Name="True Crime"
                    },
                   new Genre
                    {
                        Name="Noval"
                    },
                    new Genre
                    {
                        Name="Romance"
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        AuthorName ="Eric",
                        AuthorSurname="Ries",
                        AuthorDob= new DateTime(1978,09,22)
                    },
                    new Author
                    {
                        AuthorName ="Charlotte Perkins",
                        AuthorSurname="Stetson",
                        AuthorDob= new DateTime(1935,08,17)
                    },
                    new Author
                    {
                        AuthorName ="Frank",
                        AuthorSurname="Herbert",
                        AuthorDob= new DateTime(1920,10,08)
                    },
                    new Author
                    {
                        AuthorName ="Michael",
                        AuthorSurname="Capuzzo",
                        AuthorDob= new DateTime(1957,05,01)
                    }
                );

                context.Books.AddRange( 
                    new Book
                    {
                        //Id=1,
                        Title="Lean Startup",
                        GenreId=1,//Personal Growth
                        AuthorId=1,
                        PageCount=200,
                        PublishDate=new DateTime(2001,06,12),
                        
                    },
                    new Book
                    {
                        //Id=2,
                        Title="Herland",
                        GenreId=2,//Science Fiction
                        AuthorId =2,
                        PageCount=250,
                        PublishDate=new DateTime(2010,05,23)
                        
                    },
                    new Book
                    {
                        //Id=3,
                        Title="Dune",
                        GenreId=2,//Science Fiction
                        AuthorId=3,
                        PageCount=540,
                        PublishDate=new DateTime(2002,12,21)
                        
                    },
                    new Book
                    {
                       // Id=4,
                        Title="The Murder Room: In which Three of the Greatest Detectives Use Forensic Science to Solve the World's Most Perplexing Cold Cases",
                        GenreId=3,//True Crime
                        AuthorId=4,
                        PageCount=464,
                        PublishDate=new DateTime(2009,01,11)
                        
                    }
                );
                context.Users.AddRange( 
                    new User
                    {
                        Name ="Özlem",
                        Surname="Özçelik",
                        Email="ozlm@gmail.com",
                        Password="1234"
                    }
                );

                context.SaveChanges();

            }
        }
    }
}