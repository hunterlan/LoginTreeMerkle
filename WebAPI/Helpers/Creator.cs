using System.Security.Cryptography;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Helpers;

public class Creator : ICreator
{
    public string CreateMerkleHashTree(UserData userData, string login, string password, string salt)
    {
        var encryptedPassword = EncryptPassword(password, salt);
        
        using SHA512 sha512 = SHA512.Create();
        var loginHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.UTF8.GetBytes(login)));
        var passwordHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.UTF8.GetBytes(encryptedPassword)));
        var lpHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(loginHash + passwordHash)));


        var firstNameHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.FirstName)));
        var lastNameHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.LastName)));
        var flnHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(firstNameHash + lastNameHash)));

        var countryHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Country)));
        var cityHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.City)));
        var ccHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(countryHash + cityHash)));

        var emailHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Email)));
        var regionHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Region ?? "")));
        var erHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(emailHash + regionHash)));

        var ageHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Age.ToString())));
        var postalCodeHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.PostalCode ?? "")));
        var apHash = Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(ageHash + postalCodeHash)));

        var phoneNumberHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.PhoneNumber ?? ""));

        return Convert.ToBase64String(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(lpHash + flnHash + ccHash + erHash + apHash + phoneNumberHash)));
    }

    public string CreateHashOnData(UserData userData)
    {
        using MD5 creator = MD5.Create();
        return Convert.ToBase64String(creator.ComputeHash(Encoding.UTF8.GetBytes(userData.FirstName + userData.LastName +
                                                   userData.City + userData.Country + userData.Email)));
    }

    private string EncryptPassword(string password, string salt)
    {
        using SHA512 sha512Hash = SHA512.Create();
        //From String to byte array
        byte[] sourceBytes = Encoding.UTF8.GetBytes(password + salt);
        byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
        return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
    }
}