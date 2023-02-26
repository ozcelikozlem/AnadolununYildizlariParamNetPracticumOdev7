using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            UpdateAuthorCommand command =new UpdateAuthorCommand(_context);
            command.AuthorId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadi");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            //arrange
            UpdateAuthorCommand command =new UpdateAuthorCommand(_context);
            UpdateAuthorModel model = new UpdateAuthorModel(){AuthorName ="Franak",AuthorSurname ="Herbeart", AuthorDob = new DateTime(1910,10,08)};
            command.Model = model;
            command.AuthorId=3;
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var author=_context.Authors.SingleOrDefault(a=> a.Id == command.AuthorId);
            author.Should().NotBeNull();
            author.AuthorName.Should().Be(model.AuthorName);
            author.AuthorSurname.Should().Be(model.AuthorSurname);
            author.AuthorDob.Should().Be(model.AuthorDob);
        }
    }
}