using System.Text.RegularExpressions;

namespace JewelleryStoreManagementSystem.Data.Services
{
    public static class ValidationService
    {
        public static bool IsValidPassword(this string password)
        {
            string validPassword = @"^[a-zA-Z0-9!@#$%^&*()]{5,}$";
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

        public static bool IsValidFullName(this string fullName)
        {
            string validFullName = @"^[A-Z][a-zA-Z]*\s[A-Z][a-zA-Z]*$";
            return Regex.IsMatch(fullName, validFullName);
        }

        public static bool IsValidString(this string input)
        {
            string validInput = @"^[A-Za-z\s]+$";
            return Regex.IsMatch(input, validInput);
        }

        public static bool IsValidInteger(this string input)
        {
            return int.TryParse(input, out _);
        }
    }
}
