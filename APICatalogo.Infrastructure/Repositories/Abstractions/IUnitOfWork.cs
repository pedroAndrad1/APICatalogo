using APICatalogo.Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Infrastructure.Repositories.Abstractions
{
    public interface IUnitOfWork
    {
        public ICategoriaRepository CategoriaRepository { get; }
        public IProdutoRepository ProdutoRepository { get; }

        Task CommitAsync(CancellationToken cancellationToken);
        void Dispose();

    }
}
