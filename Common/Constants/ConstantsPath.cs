using System.Web.Configuration;

namespace Common
{
    public class ConstantsPath
    {
        public static readonly string seret_key = WebConfigurationManager.AppSettings["API_SECRET_KEY"];
        public static readonly string source_mail = WebConfigurationManager.AppSettings["SOURCE_EMAIL_FABIO"];
        public static readonly string PATH_PRE_API_SEND_EMAIL = "email/send-email-account-confirm";
        //public static readonly string PATH_PRE_API_SEND_EMAIL = "default-common-setting/get-all";
    }
}
