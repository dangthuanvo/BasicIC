using BasicIC.Services.Prototype;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Kafka
{
    public class KafkaBaseModel
    {
        public Guid? id { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }

    public class KafkaReceiveMessageModel : KafkaBaseModel, IPrototype
    {
        public string toEmail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public KafkaReceiveMessageModel()
        {
            DateTime currentDateTime = DateTime.Now;
            create_time = currentDateTime;
            modify_time = currentDateTime;
            id = Guid.NewGuid();
        }

        /// <summary>
        /// Type: Implement method
        /// Description: Method to clone object
        /// Owner: trint
        /// Create log:     15.11.2022 - trint     
        /// </summary>
        /// <returns></returns>
        public IPrototype Clone()
        {
            return this.MemberwiseClone() as IPrototype;
        }

        /// <summary>
        /// Type: Implement method
        /// Description: Method to deep clone object
        /// Owner: trint
        /// Create log:     15.11.2022 - trint     
        /// </summary>
        /// <returns></returns>
        public IPrototype DeepClone()
        {
            var data = JsonConvert.SerializeObject(this);
            var copy = JsonConvert.DeserializeObject<KafkaReceiveMessageModel>(data);
            return copy;
        }
    }

    public class ListKafkaReceiveMessageModel
    {
        public List<KafkaReceiveMessageModel> listKafkaModel { get; set; } = new List<KafkaReceiveMessageModel>();
        public string type { get; set; }
    }

    public class WebchatSendMessageModel : KafkaReceiveMessageModel
    {
        [Required]
        public string url_website { get; set; }
        public string content_text_qa { get; set; }
    }

    public class KafkaReceiveStatusModel : KafkaBaseModel
    {
        public KafkaReceiveStatusModel()
        {
            create_time = DateTime.Now;
        }
    }

    public class KafkaConversationMemberModel
    {
        public string username { get; set; }
        public string user_state { get; set; }
    }

    public class KafkaWebhookMessageModel
    {
        public string channel_type { get; set; }
        public string body { get; set; }
        public string header { get; set; }
        public string page_social_id { get; set; }
    }


    //public class KafkaSaveMessageModel : KafkaBaseModel
    //{
    //    public bool is_agent_ic { get; set; }
    //    public bool is_active { get; set; }
    //    public List<KafkaConversationMemberModel> list_member { get; set; } = new List<KafkaConversationMemberModel>();
    //    public string from_user { get; set; }

    //    public KafkaSaveMessageModel()
    //    {
    //        id = Guid.NewGuid();
    //        var now = DateTime.Now;
    //        create_time = now;
    //        modify_time = now;
    //        sent_time = now;
    //    }
    //}
}