using APICatalogo.Application.Mapping;
using APICatalogo.Domain.Repositories;
using APICatalogo.Infrastructure.Context;
using APICatalogo.Infrastructure.Repositories;
using AutoMapper;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Tests.Application.TestConfig.Produto
{
    public class ProdutoTestConfig
    {
        public readonly Faker _faker = new("pt_BR");
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public ProdutoTestConfig()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions.UseInMemoryDatabase("APICatologoTests");
            var dbContext = new AppDbContext(dbContextOptions.Options);
            var unitOfWork = new UnitOfWork(dbContext);
            _unitOfWork = unitOfWork;

            var profile = new DomainToDTOMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);

        }
    }
}
