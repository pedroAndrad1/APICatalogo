using APICatalogo.Application.Commands.Produto;
using APICatalogo.Application.DTOs;
using APICatalogo.Application.Mapping;
using APICatalogo.Domain.models;
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
    public class DeleteProdutoCommandTests
    {
        private readonly DeleteProdutoCommand.DeleteProdutoCommandHanlder _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestOutputHelper _outputHelper;

        public DeleteProdutoCommandTests(ITestOutputHelper outputHelper)
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions.UseInMemoryDatabase("APICatologoTests");
            var dbContext = new AppDbContext(dbContextOptions.Options);
            var unitOfWork = new UnitOfWork(dbContext);
            _unitOfWork = unitOfWork;

            var profile = new DomainToDTOMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);


            _handler = new DeleteProdutoCommand.DeleteProdutoCommandHanlder(unitOfWork, mapper);
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GivenParamsThenReturnADeletedProdutoDTO()
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

            DeleteProdutoCommand command = new()
            {
                Id = expectedProduto.Id
            };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ProdutoDTO>();
        }

        [Fact]
        public async Task GivenANonExistentIdThenReturnNull()
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

            DeleteProdutoCommand command = new()
            {
                Id = new Guid()
            };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeNull();
        }
    }
}
