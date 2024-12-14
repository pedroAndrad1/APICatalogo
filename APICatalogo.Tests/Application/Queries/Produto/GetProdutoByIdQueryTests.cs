using APICatalogo.Application.Mapping;
using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace APICatalogo.Tests.Application.Queries.Produto
{
    public class GetProdutoByIdQueryTests
    {
        private readonly GetProdutoByIdQuery.GetProdutoByIdQueryHandler _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestOutputHelper _outputHelper;

        public GetProdutoByIdQueryTests(ITestOutputHelper outputHelper)
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions.UseInMemoryDatabase("APICatologoTests");
            var dbContext = new AppDbContext(dbContextOptions.Options);
            var unitOfWork = new UnitOfWork(dbContext);
            _unitOfWork = unitOfWork;

            var profile = new DomainToDTOMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);


            _handler = new GetProdutoByIdQuery.GetProdutoByIdQueryHandler(unitOfWork, mapper);
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GivenAnIdThenReturnTheProductWithTheId()
        {
            // Arrange
            ProdutoModel expectedProduto = new()
            {
                Nome = _faker.Commerce.Product(),
                CategoriaId = new Guid(),
                Created_at = DateTime.UtcNow,
                Descricao = _faker.Commerce.ProductDescription(),
                PrecoEmCentavos = _faker.Random.Int(1000, 500000),
                Estoque = _faker.Random.Int(10, 200),
            };
            _unitOfWork.ProdutoRepository.Create(expectedProduto);
            await _unitOfWork.CommitAsync(CancellationToken.None);
            GetProdutoByIdQuery query = new GetProdutoByIdQuery()
            {
                Id = expectedProduto.Id,
            };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.Id.Should().Be(expectedProduto.Id);
        }

        [Fact]
        public async Task GivenAnIdThatNotExistsThenReturnNull()
        {
            // Arrange
            GetProdutoByIdQuery query = new GetProdutoByIdQuery()
            {
                Id = new Guid(),
            };

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.Should().Be(null);
        }
    }
}
