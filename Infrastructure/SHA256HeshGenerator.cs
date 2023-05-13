using System.Security.Cryptography;
using System.Text;

namespace WriteAndReadTextFile;

public static class SHA256HeshGenerator
{
    public static string ComputeSHA256(string s)
    {
        string hash = String.Empty;
 
        // Initialize a SHA256 hash object
        using (SHA256 sha256 = SHA256.Create())
        {
            // Compute the hash of the given string
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
 
            // Convert the byte array to string format
            foreach (byte b in hashValue) {
                hash += $"{b:X2}";
            }
        }
 
        return hash;
    }
}