﻿using APICatalogo.Application.Commands.Produto;
using APICatalogo.Application.DTOs;
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

namespace APICatalogo.Tests.Application.Commands.Produto
{

    public class UpdateProdutoCommandTests
    {
        private readonly UpdateProdutoCommand.UpdateProdutoCommandHander _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestOutputHelper _outputHelper;

        public UpdateProdutoCommandTests(ITestOutputHelper outputHelper)
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions.UseInMemoryDatabase("APICatologoTests");
            var dbContext = new AppDbContext(dbContextOptions.Options);
            var unitOfWork = new UnitOfWork(dbContext);
            _unitOfWork = unitOfWork;

            var profile = new DomainToDTOMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);


            _handler = new UpdateProdutoCommand.UpdateProdutoCommandHander(unitOfWork, mapper);
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GivenParamsThenReturnAnUpdatedProdutoDTO()
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

            var expectedPrecoEmCentavos = _faker.Random.Int(1000, 500000);

            UpdateProdutoCommand command = new()
            {
                Id = expectedProduto.Id,
                Nome = expectedProduto.Nome,
                PrecoEmCentavos = expectedPrecoEmCentavos,
                CategoriaId = expectedProduto.CategoriaId,
                Descricao = expectedProduto.Descricao,
                Estoque = expectedProduto.Estoque,
                ImageUrl = expectedProduto.ImageUrl,
            };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ProdutoDTO>();
            response!.PrecoEmCentavos.Should().Be(expectedPrecoEmCentavos);
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

            var expectedPrecoEmCentavos = _faker.Random.Int(1000, 500000);

            UpdateProdutoCommand command = new()
            {
                Id = new Guid(),
                Nome = expectedProduto.Nome,
                PrecoEmCentavos = expectedPrecoEmCentavos,
                CategoriaId = expectedProduto.CategoriaId,
                Descricao = expectedProduto.Descricao,
                Estoque = expectedProduto.Estoque,
                ImageUrl = expectedProduto.ImageUrl,
            };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeNull();
        }
    }
}
