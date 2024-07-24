using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.DataModels.Response.Chat
{
    public class ChatModel
    {
        public int ConversationId {  get; set; }

        public string JobName { get; set; } = string.Empty;

        public string CompanyName {  get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Messages { get; set; } = string.Empty;

        //public List<MessageModel>? Messages { get; set; }
    }
}
