using APICatalogo.Application.Mapping;
using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories;
using APICatalogo.Tests.Application.TestConfig.Produto;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace APICatalogo.Tests.Application.Queries.Produto
{
    public class GetProdutoByIdQueryTests : IClassFixture<ProdutoTestConfig>
    {
        private readonly GetProdutoByIdQuery.GetProdutoByIdQueryHandler _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestOutputHelper _outputHelper;

        public GetProdutoByIdQueryTests(ProdutoTestConfig produtoTestConfig, ITestOutputHelper outputHelper)
        {
            _handler = new GetProdutoByIdQuery.GetProdutoByIdQueryHandler(produtoTestConfig._unitOfWork, produtoTestConfig._mapper);
            _faker = produtoTestConfig._faker;
            _unitOfWork = produtoTestConfig._unitOfWork;
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
