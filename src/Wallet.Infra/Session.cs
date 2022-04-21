using Wallet.Domain.Entities.User;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;

namespace Wallet.Infra;

public class Session : ISession

{
    private readonly IUserRepository _userRepository;
    public User User { get; private set; } = new();
        
    
    public Session(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Load(Guid userId)
    {
        User = AsyncUtil.RunSync(async () => await _userRepository.GetAsync(userId))!;
    }

}