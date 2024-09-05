using APICatalogo.Application.CustomAttributeValidation;
using APICatalogo.Application.DTOs;
using APICatalogo.Domain.models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.Abstractions
{
    public class ProdutoCommand : IRequest<ProdutoDTO>, IValidatableObject
    {
        public Guid? Id { get; init; }
        [Required]
        [StringLength(80)]
        //[PrimeiraLetraMaiuscula]
        public string Nome { get; init; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string Descricao { get; init; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Valor mínimo de um centavo.")]
        public int PrecoEmCentavos { get; init; }
        [Required]
        [StringLength(300)]
        public string ImageUrl { get; init; } = string.Empty;
        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Valor mínimo de zero itens em estoque.")]
        public float Estoque { get; init; }
        [Required]
        public Guid CategoriaId { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string primeiraLetra = this.Nome.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra deve ser maiúscula.", new[] { nameof(this.Nome) });

            }

            if(this.Estoque < 0)
            {
                yield return new ValidationResult("Valor mínimo de zero itens em estoque.", new[] { nameof(this.Estoque) });
            }

        }
    }
}
