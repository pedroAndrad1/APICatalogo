using APICatalogo.Application.Abstractions;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class UpdateProdutoCommand : ProdutoCommand
    {
        public class UpdateProdutoCommandHander : IRequestHandler<UpdateProdutoCommand, ProdutoDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateProdutoCommandHander(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProdutoDTO?> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
            {
                var produtoToBeUpdated = _unitOfWork.ProdutoRepository.GetById((Guid)request.Id);
                if (produtoToBeUpdated == null) return null;

                produtoToBeUpdated.Nome = request.Nome;
                produtoToBeUpdated.Descricao = request.Descricao;
                produtoToBeUpdated.PrecoEmCentavos = request.PrecoEmCentavos;
                produtoToBeUpdated.ImageUrl = request.ImageUrl;
                produtoToBeUpdated.Estoque = request.Estoque;
                produtoToBeUpdated.CategoriaId = request.CategoriaId;
                _unitOfWork.ProdutoRepository.Update(produtoToBeUpdated);
                await _unitOfWork.CommitAsync(cancellationToken);

                var produtoToBeUpdatedDTO = _mapper.Map<ProdutoDTO>(produtoToBeUpdated);

                return produtoToBeUpdatedDTO;
            }
        }
    }
}
