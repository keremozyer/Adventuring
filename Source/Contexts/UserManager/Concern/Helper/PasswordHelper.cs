using System.Security.Cryptography;
using System.Text;

namespace Adventuring.Contexts.UserManager.Concern.Helper;

/// <summary>
/// Contains helper methods related to user passwords.
/// </summary>
public static class PasswordHelper
{
    private const int KeySize = 64;
    private const int IterationCount = 350000;

    /// <summary>
    /// Will hash the given <paramref name="password"/>. If <paramref name="salt"/> is specified it will be used in the hashing process. If <paramref name="salt"/> is not specified a new one will be generated and returned as a part of the result.
    /// </summary>
    /// <param name="password">Password to hash.</param>
    /// <param name="salt">Salt to use. If left null a new one will be generated.</param>
    /// <returns>Hashed password and the salt to use in hashing process.</returns>
    public static (string HashedText, byte[] Salt) HashPassword(string password, byte[]? salt = null)
    {
        salt ??= RandomNumberGenerator.GetBytes(KeySize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, IterationCount, HashAlgorithmName.SHA512, KeySize);

        return (Convert.ToHexString(hash), salt);
    }
}