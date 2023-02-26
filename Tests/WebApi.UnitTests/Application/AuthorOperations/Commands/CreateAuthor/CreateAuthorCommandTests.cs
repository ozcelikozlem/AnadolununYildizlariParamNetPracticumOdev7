using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExitsAuthorFullNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var author = new Author()
        {
            AuthorName = "Michaele",
            AuthorSurname = "Capuzzoo",
            AuthorDob = new DateTime(1957,05,01)
        };
        context.Authors.Add(author);
        context.SaveChanges();

        CreateAuthorCommand command = new(context, mapper);
        command.Model = new CreateAuthorModel { AuthorName = author.AuthorName, AuthorSurname = author.AuthorSurname, AuthorDob = author.AuthorDob };

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Zaten Mevcut");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        // arrange
        CreateAuthorCommand command = new(context, mapper);
        CreateAuthorModel model = new CreateAuthorModel()
        {
            AuthorName ="Michaele",
            AuthorSurname="Capuzzoe",
            AuthorDob= new DateTime(1957,05,01)
        };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var author = context.Authors.SingleOrDefault(g => g.AuthorName == model.AuthorName && g.AuthorSurname == model.AuthorSurname);
        author.Should().NotBeNull();
        author.AuthorName.Should().Be(model.AuthorName);
        author.AuthorSurname.Should().Be(model.AuthorSurname);
        author.AuthorDob.Should().Be(model.AuthorDob);
        }
    }
}