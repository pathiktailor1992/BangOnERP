using NETCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangOnERP.GlobalEncryptionDecryption
{
    public static class EncryptionDecryption
    {
        public static string KeySpec= "123456789123456789123456";
        public static string Encryption(string src)
        {
            
            return EncryptProvider.DESEncrypt(src,KeySpec);
        }

        public static string Decryption(string src)
        {
            return EncryptProvider.DESDecrypt(src,KeySpec);
        }
    }
}
