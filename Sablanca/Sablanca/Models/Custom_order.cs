using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sablanca.Models
{
    public class Custom_order
    {
        public int order_id { get; set; }
        public string user_id { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public decimal amount { get; set; }
    }
}