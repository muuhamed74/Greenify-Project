using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricultural.Core.Models
{
    public class Image_Analysis : BaseEntity
    {
        public int ImageId { get; set; }
        public string Analysis_Type { get; set; }
        public string Result { get; set; }
    }
}
