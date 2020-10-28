using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static ViewModel.Enums;

namespace ViewModel
{
    public class Utility
    {
        public class Response
        {
            public ResponseStatus Status { get; set; }
            public string Message { get; set; }
            public object ResultData { get; set; }
        }

        public static Regex Email()
        {
            const string emailPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";

            return new Regex(emailPattern, (RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture));
        }

        public static bool BeValid(string password)
        {
            bool isValid = true;

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(password) || password.Length < PasswordRules.RequiredLength)
            {
                isValid = false;
                //errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, RequiredLength));
            }
            else if (PasswordRules.RequireNonAlphanumeric && password.All(IsLetterOrDigit))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireNonLetterOrDigit);
            }
            else if (PasswordRules.RequireDigit && password.All(c => !IsDigit(c)))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireDigit);
            }
            else if (PasswordRules.RequireLowercase && password.All(c => !IsLower(c)))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireLower);
            }
            else if (PasswordRules.RequireUppercase && password.All(c => !IsUpper(c)))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireUpper);
            }

            return isValid;
        }

        public static List<T> MapToList<T>(DbDataReader dr) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(x => x.Name.ToUpper(), x => x);
                while (dr.Read())
                {
                    T newObject = new T();
                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper()))
                        {
                            var info = propDict[dr.GetName(index).ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr.GetValue(index);
                                info.SetValue(newObject, (val == DBNull.Value ? null : val), null);
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }

        public static class PasswordRules
        {
            public static int RequiredLength = 8;
            public static bool RequireLowercase = true;
            public static bool RequireUppercase = true;
            public static bool RequireDigit = true;
            public static bool RequireNonAlphanumeric = false;
        }

        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private static bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        private static bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }


        public static string ErrorMsg
        {
            get
            {
                List<string> errors = new List<string>();
                errors.Add("Password must have:");
                errors.Add($"at least {PasswordRules.RequiredLength} characters");

                if (PasswordRules.RequireNonAlphanumeric)
                {
                    errors.Add($"at least one special character (e.g. !@#$%^&*)");
                }
                if (PasswordRules.RequireDigit)
                {
                    errors.Add($"at least one digit (0-9)");

                }
                if (PasswordRules.RequireLowercase)
                {
                    errors.Add($"at least one lower case character (a-z)");
                }
                if (PasswordRules.RequireUppercase)
                {
                    errors.Add($"at least one upper case character (A-Z)");
                }

                return String.Join("\n", errors);
            }
        }
    }

    public static class StaticUtils
    {
        public static readonly string BaseUrl = "api/PackagesAndPayments/";
        public static readonly string Anonymous = "Anonymous";
        public static readonly string Unknown = "Unknown";

    }
}
