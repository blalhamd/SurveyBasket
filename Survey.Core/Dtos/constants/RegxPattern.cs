namespace Survey.Core.Dtos.constants
{
    public static class RegxPattern
    {
        public const string PasswordPattern = @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}";
        public const string FirstNamePattern = "[a-zA-Z ]{2,100}";
        public const string LastNamePattern = "[a-zA-Z ]{2,100}";
    }
}
