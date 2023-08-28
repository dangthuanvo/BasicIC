using System.Web.Configuration;

namespace Common
{
    public class ConstantsPath
    {
        public static readonly string seret_key = WebConfigurationManager.AppSettings["API_SECRET_KEY"];
        public static readonly string source_mail = WebConfigurationManager.AppSettings["SOURCE_EMAIL_FABIO"];
        public static readonly string PATH_PRE_API_SEND_EMAIL = "https://localhost:44333/api/email-service/send-email-account-confirm";
    }
}
