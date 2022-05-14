using MediatR;
using Wallet.Domain.Commands.Requests;
using Wallet.Domain.Commands.Responses;
using Wallet.Domain.Helpers;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Handlers;

public class AuthenticationHandler : Handler, IRequestHandler<AuthCommand, ResultData<AuthResponse>>
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public AuthenticationHandler(ITokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ResultData<AuthResponse>> Handle(AuthCommand command, CancellationToken cancellationToken)
    {
        var result = new ResultData<AuthResponse>();

        if (command.IsInvalid)
            return result.AddNotifications(command);

        if (!await _userRepository.HasUserWithAsync(command.Document, command.Password))
            return result.AddNotification("Número de documento ou senha estão incorretos");

        var user = await _userRepository.GetAsync(command.Document, command.Password);
                
        return result.With(new AuthResponse(user.Id, _tokenGenerator.Generate(user, Utils.TokenKey)));
    }
}
