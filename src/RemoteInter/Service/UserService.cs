using RemoteInter.Enum;
using RemoteInter.Model;

namespace RemoteInter.Service;

public class UserService : IUserService
{
    private UserEntity? _current { get; set; }

    public void Login(UserType type)
    {
        _current = new UserEntity { Type = type };
    }

    public UserEntity Get() => _current;

    public void Logout()
    {
        _current = null;
    }

    public bool IsQa()
    {
        return _current == null ? false : _current.Type == UserType.QA;
    }

    public bool IsRd()
    {
        return _current == null ? false : _current.Type == UserType.RD;
    }

    public bool IsPm()
    {
        return _current == null ? false : _current.Type == UserType.PM;
    }
}