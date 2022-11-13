using System.Security.Cryptography;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Helpers;

public class Creator : ICreator, IDisposable
{
    private readonly SHA512 _sha512 = SHA512.Create();
    public string CreateMerkleHashTree(UserData userData, string login, string password, string salt)
    {
        var encryptedPassword = EncryptPassword(password, salt);
        var lpHash = CreateNewNode(login, encryptedPassword);
        var flnPnHash = CreateNewNode(userData.FullName, userData.PhoneNumber ?? "");
        var ccHash = CreateNewNode(userData.Country, userData.City);
        var erHash = CreateNewNode(userData.Email, userData.Region ?? "");
        var apHash = CreateNewNode(userData.Birthday.ToString("MM/dd/yyyy"), userData.PostalCode ?? "");

        return Convert.ToBase64String(
            _sha512.ComputeHash(Encoding.UTF8.GetBytes(lpHash + flnPnHash + ccHash + erHash + apHash)));
    }

    public string CreateHashOnData(UserData userData)
    {
        using MD5 creator = MD5.Create();
        return Convert.ToBase64String(creator.ComputeHash(Encoding.UTF8.GetBytes(userData.FullName + userData.Birthday +
                                                   userData.City + userData.Country + userData.Email)));
    }

    private string CreateNewNode(string firstData, string secondData)
    {
        var firstDataHashed = Convert.ToBase64String(_sha512.ComputeHash(Encoding.UTF8.GetBytes(firstData)));
        var secondDataHashed = Convert.ToBase64String(_sha512.ComputeHash(Encoding.UTF8.GetBytes(secondData)));
        return Convert.ToBase64String(
            _sha512.ComputeHash(Encoding.UTF8.GetBytes(firstDataHashed + secondDataHashed)));
    }

    private string EncryptPassword(string password, string salt)
    {
        //From String to byte array
        byte[] sourceBytes = Encoding.UTF8.GetBytes(password + salt);
        byte[] hashBytes = _sha512.ComputeHash(sourceBytes);
        return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
    }

    public void Dispose()
    {
        _sha512.Dispose();
    }
}