using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Repositories
{
    public static class HashGenerator
    {
        private const int SALT_BYTE_SIZE = 24;
        private const int HASH_BYTE_SIZE = 24;
        private const int PBKDF2_ITERATIONS = 1000;
       
        public static string GenerateSlatedHash(string data, string salt)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(data, Encoding.Unicode.GetBytes(salt));
            pbkdf2.IterationCount = PBKDF2_ITERATIONS;
   
            return Encoding.Unicode.GetString(pbkdf2.GetBytes(HASH_BYTE_SIZE));
        }

        public static string CreateNewSaltValue()
        {
            //RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            using (var csprng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[SALT_BYTE_SIZE];
                csprng.GetBytes(salt);
                return Encoding.Unicode.GetString(salt);

            }
        }

        public static string PasswordHash(string password)
        {    
            byte[] _hash;
            string salt = CreateNewSaltValue();
            //new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SALT_BYTE_SIZE]);
            _hash = new Rfc2898DeriveBytes(Encoding.Unicode.GetBytes(password), Encoding.Unicode.GetBytes(salt), PBKDF2_ITERATIONS).GetBytes(HASH_BYTE_SIZE);
            string hash = Encoding.Unicode.GetString(_hash);
            return hash;
       }

        public static string[] GetPasswordHashAndSalt(string password)
        {
            byte[]  _hash;
            string salt = CreateNewSaltValue();
            //new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SALT_BYTE_SIZE]);
            _hash = new Rfc2898DeriveBytes(Encoding.Unicode.GetBytes(password), Encoding.Unicode.GetBytes(salt), PBKDF2_ITERATIONS).GetBytes(HASH_BYTE_SIZE);
            string hash = Encoding.Unicode.GetString(_hash);
            
            string[] arr = new string[3];
            arr[0] = hash;
            arr[1] = salt;

            return arr;
        }

    }
}
