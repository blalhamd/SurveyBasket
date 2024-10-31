namespace Survey.Core.Dtos.Roles.validations
{
    public class RoleRequestAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                var permissions = (IEnumerable<string>)value!;

                if (permissions.Distinct().Count() == permissions.Count())
                    return ValidationResult.Success;

            }

            return new ValidationResult("Permissions can't be dublicated for the same role");
        }
    }
}
