using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constants
    {
        public static Dictionary<string, Type> CREATED_DYNAMIC_TYPE = new Dictionary<string, Type>();
        public enum TYPE_DATA_CAMPARE { STRING, DATE, DATE_TIME, INT, FLOAT, BOOL };
        public static readonly string CONF_CROSS_ORIGIN = "CROSS_ORIGIN";
        public static readonly string KEY_SESSION_TENANT_ID = "KEY_SESSION_TENANT_ID";
        public static readonly int SERVICE_CODE = 3;
        public static readonly string KEY_SESSION_USER_ID = "KEY_SESSION_USER_ID";
        public static readonly string KEY_SESSION_EMAIL = "KEY_SESSION_EMAIL";
        public static readonly string LOG_USER_CREATE = "Log User Create";
        public static readonly string CONF_STATE_SOURCE = "STATE_SOURCE";
        public static readonly string STATE_SOURCE_DEV = "dev";
        public static ModuleBuilder MODULE_BUILDER = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("Dynamic Assembly"), AssemblyBuilderAccess.Run).DefineDynamicModule("MainModule");
    }
}
