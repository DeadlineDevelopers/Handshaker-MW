using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handshaker.Codes.Models
{
    public class IDCardModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string HomeAddress { get; set; }
        public string OfficeAddress { get; set; }
        public string MobilePhone { get; set; }
        public string OfficePhone { get; set; }
        public string FacebookAddress { get; set; }
        public string LinkedInAddress { get; set; }              
    }
}
