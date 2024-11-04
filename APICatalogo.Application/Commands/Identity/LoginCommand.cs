using MediatR;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Application.Commands.Identity
{
    public class LoginCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Usuário obrigatório")]
        public string? Username {  get; set; }
        [Required(ErrorMessage = "Senha obrigatório")]
        public string? Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
        {
            public Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
