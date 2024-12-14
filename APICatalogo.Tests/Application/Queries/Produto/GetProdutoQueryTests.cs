using APICatalogo.Application.DTOs;
using APICatalogo.Application.Mapping;
using APICatalogo.Application.Queries.Produtos;
using APICatalogo.Application.Abstractions;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using APICatalogo.Tests.Application.TestConfig.Produto;


namespace APICatalogo.Tests.Application.Queries.Produto
{
    public class GetProdutoQueryTests : IClassFixture<ProdutoTestConfig>
    {
        private readonly GetProdutosQuery.GetProdutoQueryHandler _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestOutputHelper _outputHelper;

        public GetProdutoQueryTests(ITestOutputHelper outputHelper, ProdutoTestConfig produtoTestConfig)
        {
            _faker = produtoTestConfig._faker;
            _unitOfWork = produtoTestConfig._unitOfWork;
            _handler = new GetProdutosQuery.GetProdutoQueryHandler(produtoTestConfig._unitOfWork, produtoTestConfig._mapper);
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GivenQueryThenReturnsQueryResponseOfProdutoDTO()
        {
            // Arrange
            Guid categoriaId = Guid.NewGuid();
            var totalProducts = _faker.Random.Int(51, 100);
            for (int i = 0; i < totalProducts; i++)
            {
                ProdutoModel produtoModel = new()
                {
                    Nome = _faker.Commerce.Product(),
                    CategoriaId = categoriaId,
                    Created_at = DateTime.UtcNow,
                    Descricao = _faker.Commerce.ProductDescription(),
                    PrecoEmCentavos = _faker.Random.Int(1000, 500000),
                    Estoque = _faker.Random.Int(10, 200),
                };
                _unitOfWork.ProdutoRepository.Create(produtoModel);
            }
            await _unitOfWork.CommitAsync(CancellationToken.None);
            var query = new GetProdutosQuery()
            {
                PageNumber = 1,
                PageSize = totalProducts,
            };
  
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeOfType<QueryResponse<ProdutoDTO>>();
            result.Metadata.TotalCount.Should().Be(50);
        }
    }
}
