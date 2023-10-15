using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Cryptography
{
    #region Settings

    private static int _iterations = 2;
    private static int _keySize = 256;

    private const string _hash = "SHA1";
    private static string _salt = "s384ase90b3alri2";
    private static string _vector = "83a7awl34kj4z34q";


    #endregion

    public static string Encrypt(string value, string password)
    {
        return Encrypt<AesManaged>(value, password);
    }
    public static string Encrypt<T>(string value, string password)
            where T : SymmetricAlgorithm, new()
    {
        var asciiEncoding = new ASCIIEncoding();

        byte[] vectorBytes = asciiEncoding.GetBytes(_vector);
        byte[] saltBytes = asciiEncoding.GetBytes(_salt);
        byte[] valueBytes = asciiEncoding.GetBytes(value);

        byte[] encrypted;
        using (T cipher = new T())
        {
            PasswordDeriveBytes _passwordBytes =
                new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
            byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

            cipher.Mode = CipherMode.CBC;

            using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
            {
                using (MemoryStream to = new MemoryStream())
                {
                    using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                    {
                        writer.Write(valueBytes, 0, valueBytes.Length);
                        writer.FlushFinalBlock();
                        encrypted = to.ToArray();
                    }
                }
            }
            cipher.Clear();
        }
        return Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(string value, string password)
    {
        return Decrypt<AesManaged>(value, password);
    }
    public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
    {
        var asciiEncoding = new ASCIIEncoding();

        byte[] vectorBytes = asciiEncoding.GetBytes(_vector);
        byte[] saltBytes = asciiEncoding.GetBytes(_salt);
        byte[] valueBytes = Convert.FromBase64String(value);

        byte[] decrypted;
        int decryptedByteCount = 0;

        using (T cipher = new T())
        {
            PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
            byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

            cipher.Mode = CipherMode.CBC;

            try
            {
                using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream from = new MemoryStream(valueBytes))
                    {
                        using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                        {
                            decrypted = new byte[valueBytes.Length];
                            decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                        }
                    }
                }
            }
            catch 
            {
                return String.Empty;
            }

            cipher.Clear();
        }
        return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
    }
}