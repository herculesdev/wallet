using MediatR;
using Wallet.Domain.Helpers;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Commands.Responses;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Commands.Handlers;

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
        var response = new ResultData<AuthResponse>();

        if (command.IsInvalid)
            return response.AddNotifications(command);

        if (!await _userRepository.HasUserWithAsync(command.Document, command.Password))
            return response.AddNotification("Número de documento ou senha estão incorretos");

        var user = await _userRepository.GetAsync(command.Document, command.Password);
                
        return response.With(new AuthResponse
        {
            UserId = user.Id,
            UserToken = _tokenGenerator.Generate(user, Utils.TokenKey)
        });
    }
}
