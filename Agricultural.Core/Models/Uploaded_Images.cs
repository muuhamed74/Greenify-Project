using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agricultural.Core.Models
{
    public class Uploaded_Images : BaseEntity
    {
        public int UserId { get; set; }
        public string ImagePath { get; set; }
    }
}
