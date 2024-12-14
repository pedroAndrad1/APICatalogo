using APICatalogo.Application.Abstractions;
using APICatalogo.Application.Commands.Produto;
using APICatalogo.Application.DTOs;
using APICatalogo.Application.Mapping;
using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.Repositories;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace APICatalogo.Tests.Application.Commands.Produto
{
    public class AddProdutoCommandTests
    {
        private readonly AddProdutoCommand.AddProdutoCommandHandler _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly ITestOutputHelper _outputHelper;

        public AddProdutoCommandTests(ITestOutputHelper outputHelper)
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions.UseInMemoryDatabase("APICatologoTests");
            var dbContext = new AppDbContext(dbContextOptions.Options);
            var unitOfWork = new UnitOfWork(dbContext);

            var profile = new DomainToDTOMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            _handler = new AddProdutoCommand.AddProdutoCommandHandler(unitOfWork, mapper);
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GivenProdutoParamsThenReturnProdutoDTO()
        {
            // Arrange
            AddProdutoCommand command = new()
            {
                CategoriaId = new Guid(),
                Descricao = _faker.Commerce.ProductDescription(),
                PrecoEmCentavos = _faker.Random.Int(1000, 500000),
                Estoque = _faker.Random.Int(10, 200),
            };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ProdutoDTO>();
        }
    }
}
