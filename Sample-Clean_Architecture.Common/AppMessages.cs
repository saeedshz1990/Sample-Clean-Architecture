namespace Sample_Clean_Architecture.Common
{
    public static class AppMessages
    {
        public const string SUCCESS = "Operation Successful";
        public const string ERROR = "Operation Error";
        public const string NOT_FOUND = "Item Not Found";
        public const string EMPTY = "";
        public const string INVALID_FILE = "Invalid File";
        public const string UN_PASSREQ = "Please Enter User Name And Password";
        public const string USER_NOT_FOUND = "User Not Found";
        public const string UN_PASS_INVALID = "Invalid User Name Or Password";
        public const string USER_REGISTER_SUCCESS = "User Register Successful";
        public const string USER_EXISTS = "User already Exists";
        public const string ACCOUNT_ACTIVE_SUCCESS = "Account Active Successfully";
        public const string USER_ACTIVATED = "User Already Activated";
        public const string INVALID_LINK = "Invalid Link";
        public const string USER_NOT_ACTIVATED = "User Not Activated";
        public const string UNABLE_DELETE = "Unable To Delete";
        public const string DELETE_CONFIRM = "Are you sure you want to delete this?";

        public const string VOUCHER_NOPRIVILEGE = "You don't have any privilege to Edit this voucher";
        public const string VOUCHER_NOPRIVILEGE_DATE = "You don't have any privilege to Insert the voucher at desired date";
        public const string VOUCHER_NO_DATA = "NO DATA";
        public const string VOUCHER_SAME_RECORD = "Current Voucher Is The Same Record That We Want";
        public const string UNKNOWN = "UNKNOWN ERROR";
        public const string REQUIRED = "Invalid Data";

        public static string GetMessageColor(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Success:
                    return "green";
                case MessageType.Warning:
                    return "orange";
                case MessageType.Error:
                    return "red";
                default:
                    return "black";
            }
        }
    }


    public enum MessageType
    {
        Success = 0,
        Warning = 1,
        Error = 2
    }
}
