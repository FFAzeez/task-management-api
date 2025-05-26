using System.Security.Cryptography;
using System.Text;

namespace RecruitmentProcessBusiness.Helper;

public static class Utility
{
    public static string GenerateHash(this string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2")); // hex format

            return sb.ToString();
        }
    }

    public static bool VerifyHash(this string input, string expectedHash)
    {
        string inputHash = GenerateHash(input);
        return string.Equals(inputHash, expectedHash, StringComparison.OrdinalIgnoreCase);
    }
}