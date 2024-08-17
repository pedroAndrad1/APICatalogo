using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogo.Domain.models
{
    public record CategoriaModel
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;
    }
}
