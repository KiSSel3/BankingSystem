using BankServer.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;

namespace BankServer.Services
{
    public class EncoderService : IEncoderService
    {
        public string Decrypt(string inputText, string key)
        {
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(inputText);
            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ key[i % key.Length]);
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public string Encript(string inputText, string key)
        {
            byte[] inputTextBytes = Encoding.UTF8.GetBytes(inputText);
            byte[] encryptedBytes = new byte[inputTextBytes.Length];

            for (int i = 0; i < inputTextBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(inputTextBytes[i] ^ key[i % key.Length]);
            }

            return Encoding.UTF8.GetString(encryptedBytes);
        }
    }
}
