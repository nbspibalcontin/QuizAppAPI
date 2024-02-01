using System.ComponentModel.DataAnnotations;

namespace QuizApp.Request.User
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Fullname is required")]
        public string? Fullname { get; set; }

        [Range(18, int.MaxValue, ErrorMessage = "Age must be at least 18")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [PasswordRequiresUpper(ErrorMessage = "Passwords must have at least one uppercase letter ('A'-'Z').")]
        [PasswordRequiresDigit(ErrorMessage = "Passwords must have at least one digit ('0'-'9').")]
        [PasswordRequiresSpecialCharacter(ErrorMessage = "Passwords must have at least one non-alphanumeric character.")]
        public string? Password { get; set; }

        public class PasswordRequiresUpperAttribute : ValidationAttribute
        {
            public PasswordRequiresUpperAttribute()
            {
                ErrorMessage = "Passwords must have at least one uppercase letter.";
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var password = value as string;

                if (string.IsNullOrEmpty(password))
                {
                    // If the password is null or empty, it's considered valid by this attribute.
                    return ValidationResult.Success;
                }

                if (!password.Any(char.IsUpper))
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }

        public class PasswordRequiresDigitAttribute : ValidationAttribute
        {
            public PasswordRequiresDigitAttribute()
            {
                ErrorMessage = "Passwords must have at least one digit ('0'-'9').";
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var password = value as string;

                if (string.IsNullOrEmpty(password))
                {
                    // If the password is null or empty, it's considered valid by this attribute.
                    return ValidationResult.Success;
                }

                if (!password.Any(char.IsDigit))
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }

        public class PasswordRequiresSpecialCharacterAttribute : ValidationAttribute
        {
            public PasswordRequiresSpecialCharacterAttribute()
            {
                ErrorMessage = "Passwords must have at least one non-alphanumeric character.";
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var password = value as string;

                if (string.IsNullOrEmpty(password))
                {
                    // If the password is null or empty, it's considered valid by this attribute.
                    return ValidationResult.Success;
                }

                if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    // If there is no non-alphanumeric character, the password is considered invalid.
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }
    }
}
