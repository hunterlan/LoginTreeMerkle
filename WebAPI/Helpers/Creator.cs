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
        var loginHash = Encoding.Default.GetString(sha512.ComputeHash(Encoding.UTF8.GetBytes(login)));
        var passwordHash = Encoding.Default.GetString(sha512.ComputeHash(Encoding.UTF8.GetBytes(encryptedPassword)));
        var lpHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(loginHash + passwordHash)));


        var firstNameHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.FirstName)));
        var lastNameHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.LastName)));
        var flnHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(firstNameHash + lastNameHash)));

        var countryHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Country)));
        var cityHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.City)));
        var ccHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(countryHash + cityHash)));

        var emailHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Email)));
        var regionHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Region ?? "")));
        var erHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(emailHash + regionHash)));

        var ageHash = Encoding.Default.GetString(sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.Age.ToString())));
        var postalCodeHash = Encoding.Default.GetString(sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.PostalCode ?? "")));
        var apHash = Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(ageHash + postalCodeHash)));

        var phoneNumberHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(userData.PhoneNumber ?? ""));

        return Encoding.Default.GetString(
            sha512.ComputeHash(Encoding.UTF8.GetBytes(lpHash + flnHash + ccHash + erHash + apHash + phoneNumberHash)));
    }

    public string CreateHashOnData(UserData userData)
    {
        using MD5 creator = MD5.Create();
        return Encoding.Default.GetString(creator.ComputeHash(Encoding.UTF8.GetBytes(userData.FirstName + userData.LastName +
                                                   userData.City + userData.Country + userData.Email)));
    }

    private string EncryptPassword(string password, string salt)
    {
        using SHA512 sha512Hash = SHA512.Create();
        //From String to byte array
        byte[] sourceBytes = Encoding.UTF8.GetBytes(password + salt);
        byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
        return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
    }
}