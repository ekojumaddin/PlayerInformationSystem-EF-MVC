using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerInformationSystem.Models.DTO
{
    public class MailModel
    {
        public string txtTo { get; set; }
        public string txtFrom { get; set; }
        public string txtSubject { get; set; }
        public string txtBody { get; set; }
        public string txtSmtp { get; set; }
    }
}