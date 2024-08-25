

using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.CustomAttributeValidation
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == "") return ValidationResult.Success;

            string primeiraLetra = value.ToString()[0].ToString();
            if(primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra deve ser maiúscula");
            }

            return ValidationResult.Success;
        }
    }
}
