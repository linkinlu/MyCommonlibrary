using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCSCommon.Extension
{
    public class PasswordValidator
    {
        public string EnableUpperCase { get; set; }
        public string EnalbeLowCase { get; set; }
        public string EnableFigure { get; set; }
        public string EnableSpecialChar { get; set; }
        public string MinLength { get; set; }

        public string VaildPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "Password can not be null";
            string errorMsg = string.Empty;

            if (!string.IsNullOrEmpty(this.EnableUpperCase))
            {
                var valid = Regex.IsMatch(password, @"[A-Z]");
                if (!valid)
                {
                    return "Password must contains upper case letter";
                }
            }
            if (!string.IsNullOrEmpty(this.EnalbeLowCase))
            {
                var valid = Regex.IsMatch(password, @"[a-z]");
                if (!valid)
                {
                    return "Password must contains lower case letter";
                }
            }
            if (!string.IsNullOrEmpty(this.EnableFigure))
            {
                var valid = Regex.IsMatch(password, @"[0-9]");
                if (!valid)
                {
                    return "Password must contains number";
                }
            }
            if (!string.IsNullOrEmpty(this.EnableSpecialChar))
            {

                var valid = Regex.IsMatch(password, "[^a-zA-Z0-9]+");
                if (!valid)
                {
                    return "Password must Special letter";
                }

            }
            if (!string.IsNullOrEmpty(this.MinLength))
            {
                int.TryParse(this.MinLength, out var minLength);
                if (minLength > 0 && password.Length < minLength)
                {
                    return $"Password lenght must be greater than {minLength}";
                }
            }

            return errorMsg;

        }
    }

}
