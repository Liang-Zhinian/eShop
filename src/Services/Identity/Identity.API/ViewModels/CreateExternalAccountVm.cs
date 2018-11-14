using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.ViewModels
{
    public class CreateExternalAccountVm
    {
        public string FacebookEmail { get; set; }
        public string TwitterUsername { get; set; }
        public string WechatOpenId { get; set; }
        public string AlipayUserId { get; set; }
        public string Pay2OpenId { get; set; }
    }
}
