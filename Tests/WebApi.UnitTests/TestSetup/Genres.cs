using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
            new Genre{Name="Personal Growth"},
            new Genre{Name="Science Fiction"},
            new Genre{Name="True Crime"},
            new Genre{Name="Noval"},
            new Genre{Name="Romance"});
        }
    }
}