using System;
using System.Security.Cryptography;
using System.Text;

namespace Advent_of_Code_2016
{
    public static class Day5
    {
        public static void Run()
        {
            var index = 0;
            const string input = "cxdnnyjw";
            var password = "";
            var realPassword = "".PadLeft(8, '_').ToCharArray();
            var digitsFound = 0;

            using (var md5 = MD5.Create())
            {
                while(digitsFound != 8)
                {
                    var hash = GetMd5Hash(md5, input + index++);

                    if (!hash.StartsWith("00000")) continue;

                    if (password.Length < 8)
                    {
                        password += hash[5];
                    }

                    var pos = -1;
                    if (!int.TryParse(hash[5].ToString(), out pos) || pos > realPassword.Length -1 || realPassword[pos] != '_') continue;

                    realPassword[pos] = hash[6];
                    Console.WriteLine($"Decrypting password: {string.Join("", realPassword)}");

                    digitsFound++;
                }
            }

            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Real Password: {string.Join("", realPassword)}");
        }

        
        

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
