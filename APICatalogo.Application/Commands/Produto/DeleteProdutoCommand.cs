using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using APICatalogo.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace APICatalogo.Application.Commands.Produto
{
    public class DeleteProdutoCommand : IRequest<ProdutoDTO>
    {
        public Guid Id { get; set; }

        public class DeleteProdutoCommandHanlder : IRequestHandler<DeleteProdutoCommand, ProdutoDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public DeleteProdutoCommandHanlder(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProdutoDTO?> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
            {
                var produtoToBeDeleted = _unitOfWork.ProdutoRepository.GetById(request.Id);
                if(produtoToBeDeleted == null) return null;
                

                _unitOfWork.ProdutoRepository.Delete(produtoToBeDeleted);
                await _unitOfWork.CommitAsync(cancellationToken);

                var produtoToBeDeletedDTO = _mapper.Map<ProdutoDTO>(produtoToBeDeleted);

                return produtoToBeDeletedDTO;

            }
        }
    }
}
