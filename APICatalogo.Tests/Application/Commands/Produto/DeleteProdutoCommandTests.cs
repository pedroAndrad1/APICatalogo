﻿using APICatalogo.Application.Commands.Produto;
using APICatalogo.Application.DTOs;
using APICatalogo.Application.Mapping;
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

namespace APICatalogo.Tests.Application.Commands.Produto
{
    public class DeleteProdutoCommandTests : IClassFixture<ProdutoTestConfig>
    {
        private readonly DeleteProdutoCommand.DeleteProdutoCommandHanlder _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestOutputHelper _outputHelper;

        public DeleteProdutoCommandTests(ITestOutputHelper outputHelper, ProdutoTestConfig produtoTestConfig)
        {
            _faker = produtoTestConfig._faker;
            _unitOfWork = produtoTestConfig._unitOfWork;
            _handler = new DeleteProdutoCommand.DeleteProdutoCommandHanlder(produtoTestConfig._unitOfWork, produtoTestConfig._mapper);
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
