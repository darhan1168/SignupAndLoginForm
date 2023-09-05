namespace Core;

public static class ValidationManager
{
    public static (bool, string) IsValidPassword(string password)
    {
        var errors = new List<string>();

        if (password.Length < 7)
        {
            errors.Add("Password must be at least 7 characters long.");
        }

        if (!password.Any(char.IsUpper))
        {
            errors.Add("Password must contain at least one uppercase letter.");
        }

        if (!password.Any(char.IsLower))
        {
            errors.Add("Password must contain at least one lowercase letter.");
        }

        if (!password.Any(char.IsDigit))
        {
            errors.Add("Password must contain at least one digit.");
        }

        if (errors.Count == 0)
        {
            return (true, string.Empty); 
        }
        else
        {
            return (false, string.Join(". ", errors));
        }
    }
}