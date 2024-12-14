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


namespace APICatalogo.Tests.Application.Queries.Produto
{
    public class GetProdutoQueryTests
    {
        private readonly GetProdutosQuery.GetProdutoQueryHandler _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestOutputHelper _outputHelper;

        public GetProdutoQueryTests(ITestOutputHelper outputHelper)
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions.UseInMemoryDatabase("APICatologoTests");
            var dbContext = new AppDbContext(dbContextOptions.Options);
            var unitOfWork = new UnitOfWork(dbContext);
            _unitOfWork = unitOfWork;

            var profile = new DomainToDTOMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);


            _handler = new GetProdutosQuery.GetProdutoQueryHandler(unitOfWork, mapper);
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
