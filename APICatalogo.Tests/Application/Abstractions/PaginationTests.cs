using APICatalogo.Application.Abstractions;
using Bogus;
using FluentAssertions;

namespace APICatalogo.Tests.Application.Abstractions
{
    public class PaginationTests
    {
        private readonly Faker _faker = new Faker("pt_BR");

        [Fact]
        public void ConstructorWhenInstantiedThenShouldHaveValidValues()
        {
            // Arrange
            var expectedPageNumber = int.Parse(_faker.Random.UInt(1, 10).ToString());
            var expectedPageSize = int.Parse(_faker.Random.UInt(1, 50).ToString());
            var paginationTest = new Pagination()
            {
                PageNumber = expectedPageNumber,
                PageSize = expectedPageSize,
            };
            // Act

            // Assert
            paginationTest.PageSize.Should().Be(expectedPageSize);
            paginationTest.PageNumber.Should().Be(expectedPageNumber);
        }
    }
}
