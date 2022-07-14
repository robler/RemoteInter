using RemoteInter.Enum;
using RemoteInter.Model;

namespace RemoteInter.Service;

public interface IUserService
{
    public void Login(UserType type);

    public void Logout();

    public UserEntity Get();

    public bool IsQa();

    public bool IsRd();

    public bool IsPm();
}