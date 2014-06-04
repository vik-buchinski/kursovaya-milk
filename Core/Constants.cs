namespace Core
{
    public class Constants
    {
        public const string ADMIN_ROLE = "Admin";

        public const string USER_ROLE = "User";

        public const string CURATOR_ROLE = "Curator";

        public static readonly string[] ROLES_LIST = new[] { USER_ROLE, ADMIN_ROLE, CURATOR_ROLE };

        public enum Roles { USER, ADMIN, CURATOR };

        public const string NO_USER_MESSAGE = "No such user!";

        public const string DELETE_LAST_ADMIN_MESSAGE = "You can't delete role from last admin!";

        public const string NO_ACTIVE_CONFERENCE_MESSAGE = "No active conferences!";

        public const string REGISTRATION_IS_NOT_AVAILABLE_MESSAGE = "Registration is not available!";

        public const string WRONG_ID_MESSAGE = "Wron id parameter!";

        public const string WORKS_PATH = "~/App_Data/uploads/works/";

        public const int UPLOAD_FILE_LIMIT_IN_BYTES = 52428800;

        public const string FILE_TOO_BIG_MESSAGE = " is larger than ";
    }
}