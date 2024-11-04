using MediatR;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.Commands.Identity
{
    public class RegisterCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Password { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
        {
            public Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
