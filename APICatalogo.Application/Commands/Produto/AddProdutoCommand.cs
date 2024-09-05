using APICatalogo.Application.Abstractions;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class AddProdutoCommand : ProdutoCommand
    {
        public class AddProdutoCommandHandler : IRequestHandler<AddProdutoCommand, ProdutoDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public AddProdutoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProdutoDTO> Handle(AddProdutoCommand request, CancellationToken cancellationToken)
            {
                var newProduto = new ProdutoModel
                {
                    Nome = request.Nome,
                    Descricao = request.Descricao,
                    CategoriaId = request.CategoriaId,
                    Estoque = request.Estoque,
                    PrecoEmCentavos = request.PrecoEmCentavos,
                    ImageUrl = request.ImageUrl
                };

                _unitOfWork.ProdutoRepository.Create(newProduto);
                await _unitOfWork.CommitAsync(cancellationToken);

                var newProdutoDTO = _mapper.Map<ProdutoDTO>(newProduto);

                return newProdutoDTO;

            }
        }
    }
}
