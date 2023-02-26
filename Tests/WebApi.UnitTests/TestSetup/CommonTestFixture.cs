using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddUsers();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg=> {cfg.AddProfile<MappingProfile>(); }).CreateMapper();

        }
    }
}