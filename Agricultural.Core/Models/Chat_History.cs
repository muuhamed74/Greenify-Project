using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Agricultural.Core.Models
{
    public class Chat_History : BaseEntity
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string BotResponse { get; set; }
    }
}
