namespace Survey.DataAccess.Identity.constants
{
    public static class Permissions
    {
        public static string Type { get; } = "permissions"; // property to distinct between it and between fields that will loop about it..

        public const string GetPolls = "Polls:Read";
        public const string AddPolls = "Polls:Add";
        public const string UpdatePolls = "Polls:Update";
        public const string DeletePolls = "Polls:Delete";

        public const string GetUsers = "Users:Read";
        public const string AddUsers = "Users:Add";
        public const string UpdateUsers = "Users:Update";

        public const string GetRoles = "Roles:Read";
        public const string AddRoles = "Roles:Add";
        public const string UpdateRoles = "Roles:Update";

        public static IList<string?> GetAllPermissions()
        {
            return typeof(Permissions).GetFields().Select(f=> f.GetValue(f) as string).ToList();
        }
    }
}
