using BasicIC.KafkaComponents;
using BasicIC.Models.Main;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BasicIC.Common
{
    public class CommonFunc
    {
        public static async Task LogErrorToKafka(string topic, Exception ex)
        {
            //send log kafka
            LogSystemErrorModel logMess = new LogSystemErrorModel(ex, topic);
            ProducerWrapper<LogSystemErrorModel> _producer = new ProducerWrapper<LogSystemErrorModel>();
            await _producer.CreateMess(Topic.LOG_ERROR_SYSTEM, logMess);
        }
        public static string GetMethodName(StackTrace stackTrace)
        {
            var method = stackTrace.GetFrame(0).GetMethod();

            string _methodName = method.DeclaringType.FullName;

            if (_methodName.Contains(">") || _methodName.Contains("<"))
                _methodName = _methodName.Split('<', '>')[1];
            else
                _methodName = method.Name;

            return _methodName;
        }
    }
}