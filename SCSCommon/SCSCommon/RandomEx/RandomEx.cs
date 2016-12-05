using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.RandomEx
{
    public class RandomEx
    {
        private const string Digits = "0123456789ABCDEFGHJKMNPRSTUVWXYZ";
        private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();
        public static string GenerateRandomString(int length, bool onlyDigital = false)
        {
            if (length < 1 || length > 128)
                throw new ArgumentOutOfRangeException("length");

            var result = new char[length];
            var data = new byte[length];

            _random.GetBytes(data);

            //确保首位字符始终为数字字符
            //result[0] = Digits[data[0] % 10];

            for (int i = 0; i < length; i++)
            {
                result[i] = Digits[data[i] % (onlyDigital ? 10 : 32)];
            }

            return new string(result);
        }



    }
}
