using System.Text.RegularExpressions;

namespace JewelleryStoreManagementSystem.Data.Services
{
    public static class ValidationService
    {
        public static bool IsValidPassword(this string password)
        {
            string validPassword = @"^[a-zA-Z0-9!@#$%^&*()]{8,}$";
            return Regex.IsMatch(password, validPassword);
        }

        public static bool IsValidEmail(this string email)
        {
            string validEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z]+\.[a-zA-Z.]{2,}$";
            return Regex.IsMatch(email, validEmail);
        }

        public static bool IsValidPhone(this string phone)
        {
            string validPhone = @"^04[0-9]{8}$";
            return Regex.IsMatch(phone, validPhone);
        }
    }
}
