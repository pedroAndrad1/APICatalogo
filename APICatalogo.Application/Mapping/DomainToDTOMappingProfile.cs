using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using AutoMapper;

namespace APICatalogo.Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<ProdutoModel, ProdutoDTO>()
                .ReverseMap()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            CreateMap<CategoriaModel, CategoriaDTO>()
                .ReverseMap()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
        }
    }
}
