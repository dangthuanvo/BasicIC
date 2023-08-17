using BasicIC.KafkaComponents;
using BasicIC.Models.Main;
using SocialConnection.KafkaComponents;
using System;
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
    }
}