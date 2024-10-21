using Microsoft.AspNetCore.DataProtection;
using System.Text;

namespace PG_Management_System.Helper;

public class EncryptionHelper
{
    private readonly IDataProtector _protector;

    public EncryptionHelper(IDataProtectionProvider dataProtectionProvider)
    {
        _protector = dataProtectionProvider.CreateProtector("PGManagement.Protector");
    }

    public string Encrypt(string input)
    {
        var protectedData = _protector.Protect(input);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(protectedData));  // Base64 encode the encrypted data
    }

    public string Decrypt(string cipherText)
    {
        var base64EncodedBytes = Convert.FromBase64String(cipherText);  // Decode the Base64 string
        var protectedData = Encoding.UTF8.GetString(base64EncodedBytes);
        return _protector.Unprotect(protectedData);
    }
}
