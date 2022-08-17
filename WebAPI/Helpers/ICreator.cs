using WebAPI.Models;

namespace WebAPI.Helpers;

public interface ICreator
{
    public string CreateMerkleHashTree(UserData userData, string login, string password, string salt);

    public string CreateHashOnData(UserData userData);
}