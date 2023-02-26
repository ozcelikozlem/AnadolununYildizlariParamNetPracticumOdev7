using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
            new Author{AuthorName ="Eric",AuthorSurname="Ries",AuthorDob= new DateTime(1978,09,22)},
            new Author{AuthorName ="Charlotte",AuthorSurname="Stetson",AuthorDob= new DateTime(1935,08,17)},
            new Author{AuthorName ="Frank",AuthorSurname="Herbert",AuthorDob= new DateTime(1920,10,08)},
            new Author{AuthorName ="Michael",AuthorSurname="Capuzzo",AuthorDob= new DateTime(1957,05,01)});
        }
    }
}