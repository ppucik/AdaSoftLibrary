using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Entities;
using Moq;

namespace AdaSoftLibrary.Application.UnitTests.Mocks;

public class MockAppDataContext
{
    public static Mock<IAppDataContext> GetAppXmlContext()
    {
        var library = new Library
        {
            Books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Author = "Ernest Hemingway",
                    Name = "Starec a more",
                    Year = 1952,
                    Borrowed = new Borrowed
                    {
                        FirstName = "Peter",
                        LastName = "Prvý",
                        FromDate = new DateOnly(2016, 3, 25)
                    }
                },
                new Book
                {
                    Id = 2,
                    Author = "William Shakespeare",
                    Name = "Rómeo a Júlia",
                    Year = null,
                    Borrowed = new Borrowed
                    {
                        FirstName = "Lukáš",
                        LastName = "Druhý",
                        FromDate = new DateOnly(2018, 6, 16)
                    }
                },
                new Book
                {
                    Id = 3,
                    Author = "Pavol Országh Hviezdoslav",
                    Name = "Krvavé sonety",
                    Year = null,
                    Borrowed = new Borrowed
                    {
                        FirstName = "Matej",
                        LastName = "Tretí",
                        FromDate = new DateOnly(2017, 2, 1)
                    }
                },
                new Book
                {
                    Id = 4,
                    Author = "Pavol Országh Hviezdoslav",
                    Name = "Hájniková žena",
                    Year = null,
                    Borrowed = null
                },
                new Book
                {
                    Id = 5,
                    Author = "William Shakespeare",
                    Name = "Hamlet",
                    Year = null,
                    Borrowed = new Borrowed
                    {
                        FirstName = "Jozef",
                        LastName = "Štvrtý",
                        FromDate = new DateOnly(2009, 10, 25)
                    }
                },
                new Book
                {
                    Id = 6,
                    Author = "Milo Urban",
                    Name = "Živý bič",
                    Year = null,
                    Borrowed = null
                },
            }
        };

        var mockAppXmlContext = new Mock<IAppDataContext>();
        mockAppXmlContext.Setup(x => x.Library).Returns(library);

        return mockAppXmlContext;
    }
}
