using Wallet.Domain.Entities.User;

namespace Wallet.Domain.Interfaces;

public interface ITokenGenerator
{
    string Generate(User user,string key);
}
